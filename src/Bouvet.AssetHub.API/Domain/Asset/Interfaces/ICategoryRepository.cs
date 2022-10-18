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
    public interface ICategoryRepository 
    {
        Task<Option<CategoryEntity>> Add(CategoryEntity entity);
        Task<Option<CategoryEntity>> Update(CategoryEntity entity);        
        Task<Option<List<CategoryEntity>>> GetAll();
        Task<Option<CategoryEntity>> Delete(int Id);
        
        
    }
}
