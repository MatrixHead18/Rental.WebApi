using Rental.WebApi.Features.Administrator.Domain.Entities;

namespace Rental.WebApi.Features.Administrator.Domain.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task CreateMotorcycle(Motorcycle motorcycle);
        Task UpdateMotorcycle(Motorcycle motorcycle);
        Task RemoveMotorcycle(Guid motorcycleId);
    }
}
