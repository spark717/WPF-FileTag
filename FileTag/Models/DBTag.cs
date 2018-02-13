using System.Collections.Generic;

namespace FileTag.Models
{
    public class DBTag : Tag
    {
        public int Id { get; set; }

        public virtual ICollection<DBFile> Files { get; set; }

        public DBTag()
        {
            Files = new List<DBFile>();
        }
    }
}
