using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Features.Administrator.Domain.Interfaces
{
    public interface IMotorcycleRepository : IRepositoryBase<Motorcycle>
    {
        Task UpdateMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken);
    }
}
