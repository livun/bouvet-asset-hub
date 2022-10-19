using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;
using EntityFramework.Exceptions.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Bouvet.AssetHub.API.ExtensionMethods;

namespace Bouvet.AssetHub.API.Domain.Asset.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public AssetRepository(ILogger<AssetRepository> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Option<AssetEntity>> Add(AssetEntity asset)
        {
            var category = _context.Categories.Find(asset.CategoryId);
            if (category is not null)
                asset.Category = category;

            await _context.Assets.AddAsync(asset);
            try
            {
                await _context.SaveChangesAsync();
                return asset;

            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex.Message);
                return Option<AssetEntity>.None;
            }
        }
        public async Task<Option<AssetEntity>> Update(AssetEntity asset)
        {
            var entity = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == asset.Id)
                .FirstOrDefaultAsync();
            if (entity is not null)
            {
                var category = _context.Categories.Find(asset.CategoryId);
                if (category is not null)
                    entity.Category = category;
                entity.Status = asset.Status;
                await _context.SaveChangesAsync();
                return entity;
            }

            return Option<AssetEntity>.None;

        }

        public async Task<Option<AssetEntity>> UpdateAssetStatus(int id, Status status)
        {
            var asset = _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            if (asset is not null)
            {
                asset.Status = status;
                await _context.SaveChangesAsync();
                return asset;
            }
            return Option<AssetEntity>.None;
        }



        public async Task<Option<AssetEntity>> Get(Expression<Func<AssetEntity, bool>> predicate)
        {
            return await _context.Assets
                .Include(a => a.Category)
                .AsQueryable()
                .Where(predicate)
                .FirstOrDefaultAsync();

        }
      
        public async Task<Option<List<AssetEntity>?>> GetAll()
        {
           var assets = await _context.Assets
                .Include(a => a.Category)
                .ToListAsync();
            return assets.Any() ? assets : null;
        }



        public async Task<Option<List<AssetEntity>>> GetByCategory(int categoryId)
        {
            var assets = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Category.Id == categoryId)
                .ToListAsync();
            return assets.Any() ? assets : null;
        }



        public async Task<Option<AssetEntity>> Delete(int id)
        {
            var asset = await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (asset is not null && asset.Status == Status.Registered)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
                return asset;
            }
            return Option<AssetEntity>.None;


        }
    }
}
