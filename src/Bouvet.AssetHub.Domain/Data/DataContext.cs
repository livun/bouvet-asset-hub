
using Bouvet.AssetHub.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Bouvet.AssetHub.Domain.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AssetEntity>? Assets { get; set; }
        public DbSet<LoanEntity>? Loans { get; set; }
        public DbSet<EmployeeEntity>? Employees { get; set; }
        public DbSet<CategoryEntity>? Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetEntity>()
                .Property(s => s.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));


            modelBuilder.Entity<CategoryEntity>().HasData
                (
                    new CategoryEntity { Id = 1, Name = "Developer PC" },
                    new CategoryEntity { Id = 2, Name = "Regular PC" },
                    new CategoryEntity { Id = 3, Name = "Developer MAC" },
                    new CategoryEntity { Id = 4, Name = "User MAC" },
                    new CategoryEntity { Id = 5, Name = "Exam PC" },
                    new CategoryEntity { Id = 6, Name = "Screen" },
                    new CategoryEntity { Id = 7, Name = "Keyboard" },
                    new CategoryEntity { Id = 8, Name = "Mouse" },
                    new CategoryEntity { Id = 9, Name = "Headset" }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
