using Bouvet.AssetHub.Domain.Models;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.Handlers.Helpers
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
