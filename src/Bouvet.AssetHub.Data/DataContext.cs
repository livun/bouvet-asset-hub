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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetEntity>()
                .Property(s => s.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));

            modelBuilder.Entity<AssetEntity>()
                .Property(S => S.Category)
                .HasConversion(
                    v => v.ToString(),
                    v => (Category)Enum.Parse(typeof(Category), v));

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Asset>().HasData
        //        (
        //        new Asset(1, "Computer"),
        //        new Asset(2, "Phone"),
        //        new Asset(3, "Mac")
        //        );
        //}

    }
}