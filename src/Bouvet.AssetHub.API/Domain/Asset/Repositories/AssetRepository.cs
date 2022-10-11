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

namespace Bouvet.AssetHub.API.Domain.Asset.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _log;

        public AssetRepository(DataContext context, ILogger log) 
        {
            _context = context;
            _log = log;
        }

        public async Task<Option<AssetEntity>> Add(AssetEntity entity)
        {
            await _context.Assets.AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
                return (Option<AssetEntity>)entity;

            } catch(UniqueConstraintException ex)
            {
                _log.LogError(ex.Message);
                return Option<AssetEntity>.None;
            }
        }
        public async Task<Option<AssetEntity>> Update(AssetEntity entity)
        {
            //var asset = _context.Assets
            //    .Where(a => a.Id == entity.Id)
            //    .First();
            //if (asset is not null)
            //{
            //    asset.Status = entity.Status;
            //    await _context.SaveChangesAsync();
            //    return (Option<AssetEntity>)asset;  
            //}

            return Option<AssetEntity>.None ;
           
        }

        public async Task<Option<AssetEntity>> UpdateAssetStatus(int id, Status status)
        {
            var asset = _context.Assets
                .Where(a => a.Id == id)
                .First();
            if (asset is not null)
            {
                asset.Status = status;
                await _context.SaveChangesAsync();
                return (Option<AssetEntity>)asset;
            }
            return Option<AssetEntity>.None;
        }

        public async Task<Option<AssetEntity>> Get(int id)
        {
            return await _context.Assets
                .Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Option<AssetEntity>> GetBySerialNumber(int serialNumber)
        {
            return await _context.Assets
                .Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.SerialNumber.Value == serialNumber);
        }

        public async Task<List<AssetEntity>> GetAll()
        {
           return await _context.Assets.ToListAsync();
        }
       

        public async Task<List<AssetEntity>> GetByCategory(int categoryId)
        {
            return await _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Category.Id == categoryId)
                .ToListAsync();
        }

       

        public async Task<Option<AssetEntity>> Delete(AssetEntity entity)
        {
            _context.Assets.Remove(entity);
            try
            {
                await _context.SaveChangesAsync();
                return (Option<AssetEntity>)entity;
            }
            catch (UniqueConstraintException ex)
            {
                _log.LogError(ex.Message);
                return Option<AssetEntity>.None;
            }
        }    
    }
}
