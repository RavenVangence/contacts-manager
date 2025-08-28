using Microsoft.EntityFrameworkCore;
using GeniusContactManager.Models;

namespace GeniusContactManager.Data
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use local SQL Server instance
                optionsBuilder.UseSqlServer(@"Server=CYB_LODRICKM;Database=GeniusDB;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Used).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();

                // Create index for faster searching
                entity.HasIndex(e => new { e.Name, e.Surname });
                entity.HasIndex(e => e.PhoneNumber);
            });

            // Seed some initial data
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    PhoneNumber = "123-456-7890",
                    Used = false,
                    CreatedDate = new DateTime(2023, 1, 1, 12, 0, 0)
                },
                new Contact
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Smith",
                    PhoneNumber = "987-654-3210",
                    Used = true,
                    CreatedDate = new DateTime(2023, 1, 1, 12, 0, 0)
                }
            );
        }
    }
}
