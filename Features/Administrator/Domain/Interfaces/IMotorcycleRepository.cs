using Rental.WebApi.Features.Administrator.Domain.Entities;

namespace Rental.WebApi.Features.Administrator.Domain.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task UpdateMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken);
    }
}
