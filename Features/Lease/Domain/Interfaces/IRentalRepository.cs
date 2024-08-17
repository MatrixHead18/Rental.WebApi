using Rental.WebApi.Features.Lease.Domain.Entities;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Features.Lease.Domain.Interfaces
{
    public interface IRentalRepository : IRepositoryBase<Rent>
    {
    }
}
