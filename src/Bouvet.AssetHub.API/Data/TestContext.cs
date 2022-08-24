using Bouvet.AssetHub.API.Data.Interfaces;
using Bouvet.AssetHub.API.Entities;
using MongoDB.Driver;

namespace Bouvet.AssetHub.API.Data
{
    public class TestContext : ITestContext
    {
        public TestContext(IConfiguration configuration)
        {   
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Assets = database.GetCollection<Asset>("Assets");
            TestContextSeed.SeedData(Assets);
        }

        public IMongoCollection<Asset> Assets { get; }
    }
}