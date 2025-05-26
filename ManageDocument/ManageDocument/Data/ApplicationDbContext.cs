using Microsoft.EntityFrameworkCore;
using ManageDocument.Entities;

namespace ManageDocument.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentDetail> DocumentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentNumber);

            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentNumber)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DocumentDetail>()
                .HasKey(dd => dd.AccountCode);

            modelBuilder.Entity<DocumentDetail>()
                .Property(dd => dd.AccountCode)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Document>()
                .HasMany(d => d.DocumentDetails)
                .WithOne(dd => dd.Document)
                .HasForeignKey(dd => dd.DocumentNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 