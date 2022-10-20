using Bouvet.AssetHub.API.Domain.Asset.Models;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.API.Helpers
{

    public static class AssetPredicates
    {
        public static Expression<Func<AssetEntity, bool>> ById(int id)
        {
            return (a => a.Id == id);
        }
        public static Expression<Func<AssetEntity, bool>> BySerialNumber(int serialNumber)
        {
            return (a => a.SerialNumber.Value == serialNumber);
        }
    }

}
