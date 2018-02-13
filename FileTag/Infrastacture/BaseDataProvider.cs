using AutoMapper;
using FileTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace FileTag.Infrastacture
{
    public class BaseDataProvider : IDataProvider, IDisposable
    {
        private TagDbContext _db;
        private IMapper _mapper;

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

        public void AddRelation(int fileId, int tagId)
        {
            DBFile dbFile = _db.Files.FirstOrDefault(f => f.Id == fileId);
            if (dbFile == null) return;

            DBTag dbTag = _db.Tags.FirstOrDefault(t => t.Id == tagId);
            if (dbTag == null) return;

            if (dbFile.Tags.Contains(dbTag)) return;

            dbFile.Tags.Add(dbTag);
            _db.SaveChanges();
        }

        public void AddTags(params Tag[] tags)
        {
            foreach (var tag in tags)
            {
                if (!TagExists(tag.Name))
                {
                    DBTag dbTag = _mapper.Map<DBTag>(tag);
                    _db.Tags.Add(dbTag);
                }
            }

            _db.SaveChanges();
        }

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

        public IEnumerable<DBFile> GetFiles()
        {
            return _db.Files.AsEnumerable();
        }

        public IEnumerable<string> GetFilesPaths()
        {
            return _db.Files.Select(f => f.Path).AsEnumerable();
        }

        public DBTag GetTag(string name)
        {
            return _db.Tags.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<DBTag> GetTags()
        {
            return _db.Tags.AsEnumerable();
        }

        public void RemoveTag(int tagId)
        {
            _db.Tags.Where(t => t.Id == tagId).Delete();
        }

        public bool TagExists(string name)
        {
            return _db.Tags.Any(t => t.Name == name);
        }
    }
}
