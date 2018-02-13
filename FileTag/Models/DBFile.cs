using System.Collections.Generic;

namespace FileTag.Models
{
    public class DBFile : File
    {
        public int Id { get; set; }

        public virtual ICollection<DBTag> Tags { get; set; }

        public DBFile()
        {
            Tags = new List<DBTag>();
        }
    }
}
