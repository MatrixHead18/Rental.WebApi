using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Shared.Data.Interfaces;
using Rental.WebApi.Shared.Domain.Objects;
using System.Linq.Expressions;

namespace Rental.WebApi.Shared.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        private DatabaseContext DatabaseContext;

        public RepositoryBase(DatabaseContext context)
        {
            DatabaseContext = context;
        }

        public IUnitOfWork UnitOfWork => DatabaseContext;

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().AsQueryable();

            query.Where(filter!);

            return await query.AnyAsync(cancellationToken);
        }

        public async Task DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entitySet = DatabaseContext.Set<TEntity>();
            var trackedEntity = DatabaseContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(x=> x.Entity.Id == entity.Id);
            if (trackedEntity != null)
                DatabaseContext.Entry(trackedEntity.Entity).State = EntityState.Detached;

            entitySet.Attach(entity);
        }
            
        public async Task<TEntity?> FindByIdAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = default, CancellationToken cancellationToken =default)
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().AsQueryable();

            query = query.Where(filter!);

            if (includes != null)
                query = includes(query);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().AsQueryable(); 

            query = query.Where(filter!);
            query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> InsertOneAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entitySet = DatabaseContext.Set<TEntity>();
            var entityEntry = await entitySet.AddAsync(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public async Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entitySet = DatabaseContext.Set<TEntity>();

            DatabaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
