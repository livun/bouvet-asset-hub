using Bouvet.AssetHub.API.Entities;
using MongoDB.Driver;

namespace Bouvet.AssetHub.API.Data.Interfaces
{
    public interface ITestContext
    {
        IMongoCollection<Asset> Assets {get; }
    }
}