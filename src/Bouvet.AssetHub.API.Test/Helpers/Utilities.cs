using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Models;
using Bouvet.AssetHub.API.Domain.Employee.Models;
using Bouvet.AssetHub.API.Domain.Loan.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Bouvet.AssetHub.API.Tests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DataContext db)
        {
            db.Assets.AddRange(GetSeedingAssets());
            db.Employees.AddRange(GetSeedingEmployees());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DataContext db)
        {
            db.Categories.RemoveRange(db.Categories);
            db.Assets.RemoveRange(db.Assets);
            db.Employees.RemoveRange(db.Employees);
            db.LoanHistory.RemoveRange(db.LoanHistory);
            db.Loans.RemoveRange(db.Loans);

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
                    CategoryId = 1,
                    Category = category1

                },
                new AssetEntity
                {
                    Id = 2,
                    SerialNumber = new SerialNumber { Value = 987654321 },
                    CategoryId = 2,
                    Category = category2
                },
                new AssetEntity
                {
                    Id = 3,
                    CategoryId = 3,
                    Category = category3
                },
                new AssetEntity
                {
                    Id = 4,
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


    }
}
