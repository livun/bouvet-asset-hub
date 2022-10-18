using Bouvet.AssetHub.API.Domain.Asset.Model;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Interfaces
{
    public interface IAssetRepository 
    {
        Task<Option<AssetEntity>> Add(AssetEntity entity);
        Task<Option<AssetEntity>> Update(AssetEntity entity);
        Task<Option<AssetEntity>> UpdateAssetStatus(int id, Status status);
        //Task<Option<AssetEntity>> Get(int id);
        Task<Option<AssetEntity>> Get(Expression<Func<AssetEntity, bool>> predicate);
        //Task<Option<AssetEntity>> GetBySerialNumber(int serialNumber);
        Task<Option<List<AssetEntity>>> GetAll();
        Task<Option<List<AssetEntity>>> GetByCategory(int categoryId);
        Task<Option<AssetEntity>> Delete(int Id);
        
        
    }
}
