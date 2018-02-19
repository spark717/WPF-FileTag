using FileTag.Models;
using System;
using System.Collections.Generic;

namespace FileTag.Infrastacture
{
    public interface IDataProvider: IDisposable
    {
        // TAGS
        DBTag GetTag(string name);

        /// <summary>
        /// If tag exists, find tag with same name. 
        /// If tag don't exists create new.
        /// Also add relation to exists files.
        /// </summary>
        /// <param name="tag"> model </param>
        /// <param name="releatedFiles"> related files </param>
        void TryAddTag(Tag tag, params DBFile[] relatedFiles);
        /// <summary>
        /// New tag
        /// </summary>
        /// <param name="tag"> model </param>
        /// <returns> false if tag exist </returns>
        bool NewTag(Tag tag);
        void RemoveTag(DBTag tag);
        ICollection<DBTag> GetTags();

        // FILES
        bool FileExists(string path);
        DBFile GetFile(string path);
        ICollection<DBFile> GetFiles();
        IEnumerable<string> GetFilesPaths();
        void AddFiles(params File[] files);

        // RELATION
        void RemoveRelation(DBFile file, DBTag tag);
    }
}
