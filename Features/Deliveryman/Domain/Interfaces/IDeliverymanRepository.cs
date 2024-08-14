using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Shared.Data.Interfaces;

namespace Rental.WebApi.Features.Deliveryman.Domain.Interfaces
{
    public interface IDeliverymanRepository : IRepositoryBase<DeliveryMan>
    {
        Task<bool> ExistsByCPFAsync(string cpf);
        Task<bool> ExistsByCNHNumberAsync(string cnhNumber);
    }
}
