using FileTag.Models;
using System.Data.Entity;

namespace FileTag.Infrastacture
{
    public class TagDbContext : DbContext
    {
        public TagDbContext() : base("DbConnection")
        {

        }

        public DbSet<DBTag> Tags { get; set; }
        public DbSet<DBFile> Files { get; set; }
        //public DbSet<DBWatchDir> WatchDirs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBTag>()
                .HasMany(t => t.Files)
                .WithMany(s => s.Tags)
                .Map(m =>
               {
                   m.ToTable("FileTagRelation");
                   m.MapLeftKey("TagId");
                   m.MapRightKey("FileId");
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}
