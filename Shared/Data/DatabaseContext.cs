using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NSE.Core.Mediator;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Shared.Data.EntityConfigurations;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Shared.Data
{
    public sealed class DatabaseContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IMediatorHandler mediatorHandler) : base(options) 
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Motorcycle> Motorcycles { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MotorcycleEntityTypeConfiguration());
        }


        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var rowsAffected = await base.SaveChangesAsync(cancellationToken);
            
            if (rowsAffected > 0)
            {
                await _mediatorHandler.PublicarEventos(this);
            }

            return rowsAffected;
        }
    }
}
