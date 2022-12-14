using Bouvet.AssetHub.Domain.Models;
using LanguageExt;

namespace Bouvet.AssetHub.Repositories.Interfaces
{
    public interface ICategoryRepository 
    {
        Task<Option<CategoryEntity>> Add(CategoryEntity entity);
        Task<Option<CategoryEntity>> Update(CategoryEntity entity);        
        Task<Option<List<CategoryEntity>>> GetAll();
        Task<Option<CategoryEntity>> Get(int Id);
        Task<Option<CategoryEntity>> Delete(int Id);
    }
}