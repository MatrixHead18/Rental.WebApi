using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.Data;
using Rental.WebApi.Shared.Data.Repositories;
using System.Linq.Expressions;

namespace Rental.WebApi.Features.Administrator.Adapters.Repositories
{
    public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MotorcycleRepository(DatabaseContext databaseContext) : base(databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public async Task<Motorcycle?> FindByLicensePlateAsync(string licensePlate)
        {
            Expression<Func<Motorcycle, bool>>? filter = x => x.LicensePlate == licensePlate;

            return await FindByIdAsync(filter);
        }
    }
}
