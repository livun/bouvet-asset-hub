using Bouvet.AssetHub.Domain.Models;
using LanguageExt;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.Repositories.Interfaces
{
    public interface IAssetRepository 
    {
        Task<Option<AssetEntity>> Add(AssetEntity entity);
        Task<Option<AssetEntity>> Update(AssetEntity entity);
        Task<Option<AssetEntity>> UpdateAssetStatus(int id, Status status);
        Task<Option<AssetEntity>> Get(Expression<Func<AssetEntity, bool>> predicate);
        Task<Option<List<AssetEntity>>> GetAll();
        Task<Option<List<AssetEntity>>> GetByCategory(int categoryId);
        Task<Option<AssetEntity>> Delete(int Id);        
    }
}