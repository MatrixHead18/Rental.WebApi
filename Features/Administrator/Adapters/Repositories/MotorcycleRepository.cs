using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.Data;
using Rental.WebApi.Shared.Data.Repositories;

namespace Rental.WebApi.Features.Administrator.Adapters.Repositories
{
    public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MotorcycleRepository(DatabaseContext databaseContext) : base(databaseContext) 
        {
            _databaseContext = databaseContext;
        }
    }
}
