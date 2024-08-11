using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Shared.Data;
using Rental.WebApi.Shared.Data.Repositories;

namespace Rental.WebApi.Features.Deliveryman.Adapters.Repositories
{
    public class DeliverymanRepository : RepositoryBase<DeliveryMan>
    {
        private readonly DatabaseContext _databaseContext;

        public DeliverymanRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
