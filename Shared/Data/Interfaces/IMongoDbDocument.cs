using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rental.WebApi.Shared.Data.Interfaces
{
    public interface IMongoDbDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        DateTime CreatedAt { get; }
    }
}
