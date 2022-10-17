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
using Bouvet.AssetHub.API.Domain.Asset.Predicates;

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

        public async Task<Option<AssetEntity>> Add(AssetEntity entity)
        {
            var category = _context.Categories.Find(entity.CategoryId);
            if (category is not null)
                entity.Category = category;

            await _context.Assets.AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
                return (Option<AssetEntity>)entity;

            } catch(UniqueConstraintException ex)
            {
                _logger.LogError(ex.Message);
                return Option<AssetEntity>.None;
            }
        }
        //public async Task<Option<AssetEntity>> Update(AssetEntity entity)
        //{
        //    //var asset = _context.Assets
        //    //    .Where(a => a.Id == entity.Id)
        //    //    .First();
        //    //if (asset is not null)
        //    //{
        //    //    asset.Status = entity.Status;
        //    //    await _context.SaveChangesAsync();
        //    //    return (Option<AssetEntity>)asset;  
        //    //}

        //    return Option<AssetEntity>.None ;
           
        //}

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

          

        public async Task<Option<AssetEntity>> Get(Func<AssetEntity, bool> predicate)
        {
            //return await _context.Assets
            //    .Include(a => a.Category)
            //    .Where(predicate)
            //    .AsQueryable()
            //    .FirstOrDefaultAsync();
            return _context.Assets
                .Include(a => a.Category)
                .AsQueryable()
                .Where(predicate)
                .First();

            //return await _context.Assets
            //    .Include(a => a.Category)
            //    .AsQueryable()
            //    .AsAsyncEnumerable()


            //    .()
            //    .Where(predicate)
            //    .First();


        }
        //public async Task<Option<AssetEntity>> GetBySerialNumber(int serialNumber)
        //{
        //    return await _context.Assets
        //        .Include(a => a.Category)
        //        .SingleOrDefaultAsync(a => a.SerialNumber.Value == serialNumber);
        //}

        public async Task<Option<List<AssetEntity>>> GetAll()
        {
            var assets = await _context.Assets.ToListAsync();
            if (assets.Count == 0)
                return Option<List<AssetEntity>>.None;
            return (Option<List<AssetEntity>>)assets;
        }
       

        public async Task<Option<List<AssetEntity>>> GetByCategory(int categoryId)
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
                _logger.LogError(ex.Message);
                return Option<AssetEntity>.None;
            }
        }    
    }
}
