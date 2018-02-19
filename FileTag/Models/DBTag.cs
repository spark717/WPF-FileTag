using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileTag.Models
{
    [Table("Tags")]
    public class DBTag : Tag
    {
        public int Id { get; set; }

        [NotMapped]
        public virtual ICollection<DBFile> Files { get; set; }

        public DBTag()
        {
            Files = new List<DBFile>();
        }
    }
}
