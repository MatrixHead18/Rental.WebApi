using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Deliveryman.Application.Models.Requests;

namespace Rental.WebApi.Features.Deliveryman.Application.Interfaces
{
    public interface IDeliverymanService
    {
        Task CreateDeliveryManAsync(CreateNewDeliveryManRequest model, CancellationToken cancellationToken = default);

        Task UpdateDeliveryManAsync(UpdateDeliveryManRequest model, CancellationToken cancellationToken = default);
    }
}
