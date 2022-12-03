using Bouvet.AssetHub.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Bouvet.AssetHub.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<AssetEntity> Assets => Set<AssetEntity>();
        public DbSet<LoanEntity> Loans => Set<LoanEntity>();
        public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<LoanHistoryEntity> LoanHistory => Set<LoanHistoryEntity>();
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
            //modelBuilder.Entity<EmployeeEntity>
            //    (emp =>
            //        {
            //            emp.HasData(new EmployeeEntity { Id = 1 });
            //            emp.OwnsOne(e => e.EmployeeNumber).HasData(new { EmployeeEntityId = 1, Value = 1234 });
            //        }
            //    );
            //modelBuilder.Entity<EmployeeEntity>
            //    (emp =>
            //    {
            //        emp.HasData(new EmployeeEntity { Id = 2 });
            //        emp.OwnsOne(e => e.EmployeeNumber).HasData(new { EmployeeEntityId = 2, Value = 5678 });
            //    }
            //    );


            modelBuilder.Entity<AssetEntity>
                (   asset =>
                    {
                        asset.HasData(new AssetEntity { Id = 1, CategoryId = 1 });
                        asset.OwnsOne(a => a.SerialNumber).HasData(new { AssetEntityId = 1, Value = "123456789" });
                        asset.OwnsOne(a => a.QrIdentifier).HasData(new { AssetEntityId = 1, Value = Guid.NewGuid() });
                    }
                );
            modelBuilder.Entity<AssetEntity>
               (asset =>
               {
                   asset.HasData(new AssetEntity { Id = 2, CategoryId = 1 });
                   asset.OwnsOne(a => a.SerialNumber).HasData(new { AssetEntityId = 2, Value = "987654321" });
                   asset.OwnsOne(a => a.QrIdentifier).HasData(new { AssetEntityId = 2, Value = Guid.NewGuid() });
               }
               );
            modelBuilder.Entity<AssetEntity>
               (asset =>
               {
                   asset.HasData(new AssetEntity { Id = 3, CategoryId = 2 });
                   asset.OwnsOne(a => a.SerialNumber).HasData(new { AssetEntityId = 3, Value = "678912345" });
                   asset.OwnsOne(a => a.QrIdentifier).HasData(new { AssetEntityId = 3, Value = Guid.NewGuid() });
               }
               );
            modelBuilder.Entity<AssetEntity>
               (asset =>
               {
                   asset.HasData(new AssetEntity { Id = 4, CategoryId = 6});
                   asset.OwnsOne(a => a.SerialNumber).HasData(new { AssetEntityId = 4, Value = "" });
                   asset.OwnsOne(a => a.QrIdentifier).HasData(new { AssetEntityId = 4, Value = Guid.NewGuid() });
               }
               );
            //modelBuilder.Entity<LoanEntity>
            //   (loan =>
            //   {
            //       loan.HasData(new LoanEntity { Id = 1, AssetId = 4, Borrower = { Id = 1 } });
            //       loan.OwnsOne(l => l.Interval).HasData(new { IsLongterm = false, Start = DateTime.Today, Stop = DateTime.Today.AddDays(300) });
            //       loan.OwnsOne(l => l.Bsd).HasData(new {LoanEntityId = 1, Reference = "RK3456" });
            //   }
            //   );




            base.OnModelCreating(modelBuilder);
        }

    }
}
