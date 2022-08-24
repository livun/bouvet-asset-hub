
namespace Bouvet.AssetHub.API.Entities
{
    public class Asset : MongoEntity
    {
        public string Name {get; set;} = "";
        public string Description {get; set;} = "";
        public string Category {get; set;} = "";
    }
}