using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Features.Lease.Domain.Entities;
using Rental.WebApi.Features.Lease.Domain.Interfaces;
using Rental.WebApi.Shared.Data;
using Rental.WebApi.Shared.Data.Repositories;

namespace Rental.WebApi.Features.Lease.Adapters.Repositories
{
    public class RentalRepository : RepositoryBase<Rent>, IRentalRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RentalRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
