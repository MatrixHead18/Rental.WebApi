using MongoDB.Bson;
using MongoDB.Driver;
using Rental.WebApi.Shared.Data.Attributes;
using Rental.WebApi.Shared.Data.Interfaces;
using System.Linq.Expressions;

namespace Rental.WebApi.Shared.Data.Repositories
{
    public class RepositoryBase<TDocument> : IRepositoryBase<TDocument> where TDocument : IMongoDbDocument
    {
        protected IMongoCollection<TDocument> Collection { get; init; }

        public RepositoryBase(IMongoDatabase mongoDatabase)
        {
            Collection = mongoDatabase.GetCollection<TDocument>(
                name: RepositoryBase<TDocument>.GetCollectionName(typeof(TDocument))
            );
        }

        private static string GetCollectionName(Type documentType)
        {
            var attributes = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true);

            if(attributes is not null)
            {
                var attribute = attributes.FirstOrDefault() as BsonCollectionAttribute;

                return attribute?.CollectionName ?? documentType.Name;
            }

            return string.Empty;
        }


        public async Task DeleteOneAsync(TDocument document, CancellationToken cancellationToken = default)
            => await Collection.DeleteOneAsync(doc => doc.Id == document.Id, cancellationToken);

        public async Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            var objectId = new ObjectId(id);
            var result = await Collection.FindAsync<TDocument>(f => f.Id == objectId, cancellationToken: cancellationToken);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> expression, CancellationToken cancellationToken)
            => await Collection.Find(expression).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            var result = await Collection.FindAsync<TDocument>(filter => true, cancellationToken: cancellationToken);

            return await result.ToListAsync(cancellationToken);
        }

        public async Task InsertOneAsync(TDocument document)
            => await Collection.InsertOneAsync(document);

        public async Task UpdateOneAsync(Expression<Func<TDocument, bool>> expression, UpdateDefinition<TDocument> document, UpdateOptions options, CancellationToken cancellationToken = default)
            => await Collection.UpdateOneAsync(expression, document, options, cancellationToken: cancellationToken);
    }
}
