using Bouvet.AssetHub.Domain.Data;
using Bouvet.AssetHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Repositories
{

    public class AssetRepository : IRepository<AssetEntity, int>
    {
        private readonly DataContext _context;

        public AssetRepository(DataContext context)
        {
            _context = context;
        }
        public Task<AssetEntity> Create(AssetEntity entity)
        {
            _context.Assets.Add(entity);
        }

        public Task<AssetEntity> Delete(AssetEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<AssetEntity>> Read()
        {
            throw new NotImplementedException();
        }

        public Task<AssetEntity> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssetEntity> Update(AssetEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
