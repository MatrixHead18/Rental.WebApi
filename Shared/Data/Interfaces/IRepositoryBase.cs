using Microsoft.EntityFrameworkCore.Query;
using Rental.WebApi.Shared.Domain.Interfaces;
using System.Linq.Expressions;

namespace Rental.WebApi.Shared.Data.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntityModel
    {
        public IUnitOfWork UnitOfWork { get; }
        Task<TEntity> InsertOneAsync(TEntity document, CancellationToken cancellationToken);
        Task<TEntity?> FindByIdAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = default, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);
        Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);
    }
}
