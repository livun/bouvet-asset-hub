using Bouvet.AssetHub.API.Entities;

namespace Bouvet.AssetHub.API.Repository
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAssets();
        Task<Asset> GetAsset(string id);
        Task<IEnumerable<Asset>> GetAssetByName(string name);
        Task<IEnumerable<Asset>> GetAssetByCategory(string categoryName);

     
        Task CreateAsset(Asset asset);
        Task<bool> UpdateAsset(Asset asset);
        Task<bool> DeleteAsset(string id);
        
    }
}