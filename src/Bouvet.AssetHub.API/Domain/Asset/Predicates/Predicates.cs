using Bouvet.AssetHub.API.Domain.Asset.Model;

namespace Bouvet.AssetHub.API.Domain.Asset.Predicates
{

    public class DatabaseIdentifier {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
    }
    public static class Predicate
    {
        public static Func<AssetEntity, DatabaseIdentifier, bool> BySerialNumber = ((a, identifier) => a.SerialNumber.Value == identifier.SerialNumber);
        public static Func<AssetEntity, DatabaseIdentifier,  bool> ById = ((a, identifier) => a.Id == identifier.Id);


    }
 
}
