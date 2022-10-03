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
        AssetEntity GetById(int id);
        Option<List<AssetEntity>> GetAll();
        void Add(AssetEntity entity);
        void AddRange(IEnumerable<AssetEntity> entities);
        void Remove(AssetEntity entity);
        void Save();
        //void RemoveRange(IEnumerable<AssetEntity> entities);
        
    }
}
