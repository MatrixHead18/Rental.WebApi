using Microsoft.EntityFrameworkCore;
using Rental.WebApi.Shared.Data.Interfaces;
using Rental.WebApi.Shared.Domain;
using System.Linq.Expressions;

namespace Rental.WebApi.Shared.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityModel
    {
        private DatabaseContext DatabaseContext;

        public RepositoryBase(DatabaseContext context)
        {
            DatabaseContext = context;
        }

        public IUnitOfWork UnitOfWork => DatabaseContext;

        public async Task DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entitySet = DatabaseContext.Set<TEntity>();
            var trackedEntity = DatabaseContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(x=> x.Entity.Id == entity.Id);
            if (trackedEntity != null)
                DatabaseContext.Entry(trackedEntity.Entity).State = EntityState.Detached;

            entitySet.Attach(entity);

            await UnitOfWork.SaveChangesAsync();
        }
            
        public async Task<TEntity?> FindByIdAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken =default)
        {
            IQueryable<TEntity> query = DatabaseContext.Set<TEntity>().AsQueryable();

            query = query.Where(filter!);

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

            await UnitOfWork.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entitySet = DatabaseContext.Set<TEntity>();

            DatabaseContext.Entry(entity).State = EntityState.Modified;

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
