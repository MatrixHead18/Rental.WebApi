using MongoDB.Driver;
using System.Linq.Expressions;

namespace Rental.WebApi.Shared.Data.Interfaces
{
    public interface IRepositoryBase<TDocument> where TDocument : IMongoDbDocument
    {
        Task InsertOneAsync(TDocument document);
        Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> expression, CancellationToken cancellationToken);
        Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>>? filter = null, CancellationToken cancellationToken = default);
        Task UpdateOneAsync(Expression<Func<TDocument, bool>> expression, UpdateDefinition<TDocument> document, UpdateOptions options, CancellationToken cancellationToken = default);
        Task DeleteOneAsync(TDocument document, CancellationToken cancellationToken = default);
    }
}
