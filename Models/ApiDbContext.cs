using Microsoft.EntityFrameworkCore;

namespace UniNotesAPI.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Folder> Folders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>().HasMany(f => f.Documents).WithOne(d => d.Folder).HasForeignKey(d => d.FolderId);
            // modelBuilder.Entity<Document>().HasOne(d => d.Folder).WithMany(f => f.Documents).HasForeignKey(d => d.FolderId);
        }
    }
}