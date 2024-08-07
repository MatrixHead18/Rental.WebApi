using MongoDB.Bson;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Shared.Domain
{
    public class MongoDbDocument : IMongoDbDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}
