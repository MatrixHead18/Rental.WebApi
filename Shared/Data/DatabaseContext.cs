using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Lease.Domain.Entities;
using Rental.WebApi.Shared.Data.EntityConfigurations;
using Rental.WebApi.Shared.Data.Interfaces;
using Rental.WebApi.Shared.Mediator;
using System.Data;

namespace Rental.WebApi.Shared.Data
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IMediatorHandler mediatorHandler) : base(options) 
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Motorcycle> Motorcycles { get; init; }
        public DbSet<RentPlan> RentalPlans { get; init; }
        public DbSet<Rent> Rentals { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MotorcycleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeasePlanEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaseEntityTypeConfiguration());
        }

        public async Task<int> PersistChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
           return await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await PersistChangesAsync(cancellationToken);

            if (rowsAffected > 0)
                await _mediatorHandler.PublicarEvento(this);

            await transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}
