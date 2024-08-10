using Microsoft.EntityFrameworkCore;
using Rental.WebApi.Shared.Data.EntityConfigurations;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Shared.Data
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MotorcycleEntityTypeConfiguration());
        }


        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var rowsAffected = await base.SaveChangesAsync(cancellationToken);
            return rowsAffected;
        }
    }
}
