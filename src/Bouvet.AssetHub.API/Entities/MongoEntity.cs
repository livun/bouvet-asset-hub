using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bouvet.AssetHub.API.Entities
{
    public class MongoEntity
    {
        //public ObjectId Id { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get; set; }
        public DateTime CreatedAt { get; set; }
    }
}