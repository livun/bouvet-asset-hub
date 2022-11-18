using Bouvet.AssetHub.Data;
using Bouvet.AssetHub.Domain.Models;

namespace Bouvet.AssetHub.API.Tests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DataContext db)
        {
            
            db.Assets.AddRange(GetSeedingAssets());
            db.Categories.AddRange(GetSeedingCategories());
            db.Employees.AddRange(GetSeedingEmployees());
            db.Loans.AddRange(GetSeedingLoans());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DataContext db)
        {
            db.Categories.RemoveRange(db.Categories.ToList());
            db.Assets.RemoveRange(db.Assets);
            db.Employees.RemoveRange(db.Employees);
            db.LoanHistory.RemoveRange(db.LoanHistory);
            db.Loans.RemoveRange(db.Loans);
            db.SaveChanges();

            InitializeDbForTests(db);
        }

        public static List<AssetEntity> GetSeedingAssets()
        {
            var category1 = new CategoryEntity { Id = 1, Name = "Developer PC" };
            var category2 = new CategoryEntity { Id = 2, Name = "Regular PC" };
            var category3 = new CategoryEntity { Id = 3, Name = "Headphones" };
            var category4 = new CategoryEntity { Id = 4, Name = "Mouse" };

            return new List<AssetEntity>()
            {
                new AssetEntity
                {
                    Id = 1,
                    SerialNumber = new SerialNumber { Value = 123456789 },
                    QrIdentifier = new QrIdentifier { Value = Guid.NewGuid()},
                    CategoryId = 1,
                    Category = category1

                },
                new AssetEntity
                {
                    Id = 2,
                    SerialNumber = new SerialNumber { Value = 987654321 },
                    QrIdentifier = new QrIdentifier { Value = Guid.NewGuid()},
                    CategoryId = 2,
                    Category = category2
                },
                new AssetEntity
                {
                    Id = 3,
                    QrIdentifier = new QrIdentifier { Value = Guid.NewGuid() },
                    CategoryId = 3,
                    Category = category3
                },
                new AssetEntity
                {
                    Id = 4,
                    QrIdentifier = new QrIdentifier { Value = Guid.NewGuid()},
                    CategoryId = 3,
                    Category = category3
                 }
            };
        }
        public static List<EmployeeEntity> GetSeedingEmployees()
        {
            return new List<EmployeeEntity>()
            {
                new EmployeeEntity
                {
                    EmployeeNumber = new EmployeeNumber { Value = 1234 },
                    Id = 1
                },
                new EmployeeEntity
                {
                    EmployeeNumber = new EmployeeNumber { Value = 2345 },
                    Id = 2
                },
            };
        }
        public static List<CategoryEntity> GetSeedingCategories()
        {
            return new List<CategoryEntity>
            {
                new CategoryEntity { Id = 5, Name = "Exam PC" }
            };
    }
        public static List<LoanEntity> GetSeedingLoans()
        {
            var employee3 = new EmployeeEntity
            {
                EmployeeNumber = new EmployeeNumber { Value = 3456 },
                Id = 3
            };
            var employee4 = new EmployeeEntity
            {
                EmployeeNumber = new EmployeeNumber { Value = 4567 },
                Id = 4
            };
            var category6 = new CategoryEntity { Id = 6, Name = "Screen" };
            var category7 = new CategoryEntity { Id = 7, Name = "Regular PC" };

            var asset5 = new AssetEntity
            {
                Id = 5,
                QrIdentifier = new QrIdentifier { Value = Guid.NewGuid() },
                CategoryId = 6,
                Category = category6
            };
            var asset6 = new AssetEntity
            {
                Id = 6,
                QrIdentifier = new QrIdentifier { Value = Guid.NewGuid() },
                CategoryId = 7,
                Category = category7
            };
           

            return new List<LoanEntity>()
            {
                new LoanEntity
                {
                    Id = 1,
                    Interval = new Interval { IsLongterm = true, Start = DateTime.Today, Stop = null },
                    AssignedTo = employee3.EmployeeNumber,
                    Borrower = employee3,
                    AssetId = asset5.Id,
                    Asset = asset5,
                    Bsd = new Bsd { Reference = "RK4568"}

                },
                new LoanEntity
                {
                    Id = 2,
                    Interval = new Interval { IsLongterm = false, Start = DateTime.Today, Stop = DateTime.Today.AddDays(7) },
                    AssignedTo = employee4.EmployeeNumber,
                    Borrower = employee4,
                    AssetId = asset6.Id,
                    Asset = asset6,
                },


            };
        }
    }
}


