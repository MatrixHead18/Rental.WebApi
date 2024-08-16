using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Features.Lease.Application.Interfaces;
using Rental.WebApi.Features.Lease.Application.Models.Requests;
using Rental.WebApi.Features.Lease.Application.Models.Responses;

namespace Rental.WebApi.Features.Lease.Application.Services
{
    public class LeaseService : ILeaseServices
    {
        private readonly ILogger<LeaseService> _logger;
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public LeaseService(IDeliverymanRepository deliverymanRepository,
                            IMotorcycleRepository motorcycleRepository,
                            ILogger<LeaseService> logger)
        {
            _deliverymanRepository = deliverymanRepository;
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
        }

        public async Task<LeaseResponse> RentAMotorcycleAsync(RentMotorcycleRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Rent motorcycle flow starting...");

            var deliveryManTask = _deliverymanRepository.FindByIdAsync(f => f.Id == request.IdDeliveryMan, cancellationToken: cancellationToken);
            var motorcycleTask = _motorcycleRepository.FindByLicensePlateAsync(request.LicensePlate);

            await Task.WhenAll(deliveryManTask, motorcycleTask);

            var deliveryMan = await deliveryManTask;
            var motorcycle = await motorcycleTask;

            if(deliveryMan is null || motorcycle is null)
            {
                _logger.LogWarning("There's no deliveryman or motorcycle to rent");
                return;
            }

            SetMotorcycleOwner(deliveryMan!, motorcycle);

            return await ExecuteCreationLease(deliveryMan, motorcycle);
        }

        private async Task<LeaseResponse> ExecuteCreationLease(DeliveryMan deliveryMan, Motorcycle motorcycle)
        {
            //todo: fazer transaction para criação da locação

            var lease = new Lease(deliveryMan, motorcycle);
        }

        private async void SetMotorcycleOwner(DeliveryMan deliveryMan, Motorcycle motorcycle)
        {
            _logger.LogInformation("Relationship motorcycle and his owner flow starting...");

            motorcycle.SetDeliverymanOwner(deliveryMan!.Id);

            await _motorcycleRepository.UnitOfWork.PersistChangesAsync();
        }
    }
}
