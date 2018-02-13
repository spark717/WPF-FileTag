using FileTag.Models;
using System;
using System.Collections.Generic;

namespace FileTag.Infrastacture
{
    public interface IDataProvider: IDisposable
    {
        // TAGS
        bool TagExists(string name);
        DBTag GetTag(string name);
        void AddTags(params Tag[] tags);
        void RemoveTag(int tagId);
        IEnumerable<DBTag> GetTags();

        // FILES
        bool FileExists(string path);
        DBFile GetFile(string path);
        IEnumerable<DBFile> GetFiles();
        IEnumerable<string> GetFilesPaths();
        void AddFiles(params File[] files);

        // RELATIONS
        void AddRelation(int fileId, int tagId);
    }
}
