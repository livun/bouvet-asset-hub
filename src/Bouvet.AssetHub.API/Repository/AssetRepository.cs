using Bouvet.AssetHub.API.Data.Interfaces;
using Bouvet.AssetHub.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Bouvet.AssetHub.API.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ITestContext _context;
        public AssetRepository(ITestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateAsset(Asset asset)
        {
           await _context.Assets.InsertOneAsync(asset);
        }

        public async Task<bool> DeleteAsset(string id)
        {
            FilterDefinition<Asset> filter = Builders<Asset>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Assets
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Asset> GetAsset(string id)
        {
             return await _context
                           .Assets
                           .Find(a => a.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetByCategory(string categoryName)
        {
             FilterDefinition<Asset> filter = Builders<Asset>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Assets
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetByName(string name)
        {
            FilterDefinition<Asset> filter = Builders<Asset>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                            .Assets
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _context.Assets.Find(a => true)
                                        .ToListAsync();
        }

        public async Task<bool> UpdateAsset(Asset asset)
        {
            var updateResult = await _context
                                        .Assets
                                        .ReplaceOneAsync(filter: g => g.Id == asset.Id, replacement: asset);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}