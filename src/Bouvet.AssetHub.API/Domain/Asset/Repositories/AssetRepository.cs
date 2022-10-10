using Bouvet.AssetHub.API.Data;
using Bouvet.AssetHub.API.Domain.Asset.Interfaces;
using Bouvet.AssetHub.API.Domain.Asset.Model;

using LanguageExt;
using Microsoft.EntityFrameworkCore;
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

        public AssetRepository(DataContext context) 
        {
            _context = context;
        }

        public void Add(AssetEntity entity)
        {
            _context.Assets.Add(entity);
        }

        public void AddRange(IEnumerable<AssetEntity> entities)
        {
            _context.Assets.AddRange(entities);
        }

        public Option<List<AssetEntity>> GetAll()
        {

            //Option<List<AssetEntity>> assets = _context.Assets.ToList();
            //return assets;
           return _context.Assets.ToList();



        }

        public AssetEntity GetById(int id)
        {
            return _context.Assets
                .Include(a => a.Category)
                .Where(a => a.Id == id)
                .First();
        }

        public void Remove(AssetEntity entity)
        {
            _context.Assets.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
