using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTag.Models
{
    [Table("Files")]
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
