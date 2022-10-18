using Bouvet.AssetHub.API.Domain.Asset.Model;
using System.Linq.Expressions;

namespace Bouvet.AssetHub.API.Domain.Asset.Predicates
{

    public class DatabaseIdentifier {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
    }
    public static class Predicate
    {
        //public static Func<AssetEntity, DatabaseIdentifier, bool> BySerialNumber = ((a, identifier) => a.SerialNumber.Value == identifier.SerialNumber);
        //public static Func<AssetEntity, DatabaseIdentifier,  bool> ById = ((a, identifier) => a.Id == identifier.Id);

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
