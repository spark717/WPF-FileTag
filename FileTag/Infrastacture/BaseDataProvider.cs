﻿using AutoMapper;
using FileTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileTag.Infrastacture
{
    public class BaseDataProvider : IDataProvider, IDisposable
    {
        public BaseDataProvider()
        {
            _db = new TagDbContext();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<File, DBFile>();
                cfg.CreateMap<Tag, DBTag>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        private TagDbContext _db;
        private IMapper _mapper;

        public void AddFiles(params File[] files)
        {
            foreach (var file in files)
            {
                if (!FileExists(file.Path))
                {
                    DBFile dbFile = _mapper.Map<DBFile>(file);
                    _db.Files.Add(dbFile);
                }
            }

            _db.SaveChanges();
        }

        //public void AddRelation(int fileId, int tagId)
        //{
        //    DBFile dbFile = _db.Files.FirstOrDefault(f => f.Id == fileId);
        //    if (dbFile == null) return;

        //    DBTag dbTag = _db.Tags.FirstOrDefault(t => t.Id == tagId);
        //    if (dbTag == null) return;

        //    if (dbFile.Tags.Contains(dbTag)) return;

        //    dbFile.Tags.Add(dbTag);
        //    _db.SaveChanges();
        //}

        //public void AddTags(params Tag[] tags)
        //{
        //    foreach (var tag in tags)
        //    {
        //        if (!TagExists(tag.Name))
        //        {
        //            DBTag dbTag = _mapper.Map<DBTag>(tag);
        //            _db.Tags.Add(dbTag);
        //        }
        //    }

        //    _db.SaveChanges();
        //}

        public void Dispose()
        {
            _db?.Dispose();
        }

        public bool FileExists(string path)
        {
            return _db.Files.Any(f => f.Path == path);
        }

        public DBFile GetFile(string path)
        {
            return _db.Files.FirstOrDefault(f => f.Path == path);
        }

        public ICollection<DBFile> GetFiles()
        {
            return _db.Files.ToList();
        }

        public IEnumerable<string> GetFilesPaths()
        {
            return _db.Files.Select(f => f.Path).AsEnumerable();
        }

        public DBTag GetTag(string name)
        {
            return _db.Tags.FirstOrDefault(t => t.Name == name);
        }

        public ICollection<DBTag> GetTags()
        {
            return _db.Tags.ToList();
        }

        public void RemoveTag(DBTag tag)
        {
            _db.Tags.Remove(tag);
            _db.SaveChanges();
        }

        public bool TagExists(string name)
        {
            return _db.Tags.Any(t => t.Name == name);
        }

        public void TryAddTag(Tag tag, params DBFile[] relatedFiles)
        {
            DBTag dbTag = TagExists(tag.Name) ? GetTag(tag.Name) : _db.Tags.Add(_mapper.Map<DBTag>(tag));

            if (relatedFiles != null && relatedFiles.Any())
                foreach (var dbFile in relatedFiles)
                {
                    dbTag.Files.Add(dbFile);
                }

            _db.SaveChanges();
        }
    }
}
