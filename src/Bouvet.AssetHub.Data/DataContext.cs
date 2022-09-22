using Bouvet.AssetHub.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Bouvet.AssetHub.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<AssetEntity>? Assets { get; set; }
        public DbSet<LoanEntity>? Loans { get; set; }
        public DbSet<EmployeeEntity>? Employees { get; set; }
        public DbSet<CategoryEntity>? Categories { get; set }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetEntity>()
                .Property(s => s.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));


            modelBuilder.Entity<CategoryEntity>().HasData
                (
                    new CategoryEntity { Name = "Developer PC"},
                    new CategoryEntity { Name = "Regular PC"},
                    new CategoryEntity { Name = "Developer MAC"},
                    new CategoryEntity { Name = "User MAC"},
                    new CategoryEntity { Name = "Exam PC" },
                    new CategoryEntity { Name = "Screen" },
                    new CategoryEntity { Name = "Keyboard" },
                    new CategoryEntity { Name = "Mouse" },
                    new CategoryEntity { Name = "Headset" }
                );
            base.OnModelCreating(modelBuilder);
        }
          
    }
}