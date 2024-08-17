using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Features.Lease.Application.Interfaces;
using Rental.WebApi.Features.Lease.Application.Models.Requests;
using Rental.WebApi.Features.Lease.Application.Models.Responses;
using Rental.WebApi.Features.Lease.Domain.Entities;
using Rental.WebApi.Features.Lease.Domain.Interfaces;

namespace Rental.WebApi.Features.Lease.Application.Services
{
    public class LeaseService : ILeaseServices
    {
        private readonly ILogger<LeaseService> _logger;
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public LeaseService(IDeliverymanRepository deliverymanRepository,
                            IMotorcycleRepository motorcycleRepository,
                            IRentalRepository rentalRepository,
                            ILogger<LeaseService> logger)
        {
            _deliverymanRepository = deliverymanRepository;
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
            _logger = logger;
        }

        public async Task<LeaseResponse> CalculateTotalRent(Guid idRental, DateTime devolutionDate, CancellationToken cancellationToken = default)
        {
            var rental = await _rentalRepository.FindByIdAsync(f => f.Id == idRental, cancellationToken: cancellationToken);

            rental!.CalculateRentWithValueFineTotalValue(devolutionDate);

            await _rentalRepository.UnitOfWork.PersistChangesAsync();

            return new LeaseResponse { };
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

            SetMotorcycleOwner(motorcycle!, deliveryMan);

            return await ExecuteCreationRent(deliveryMan);
        }

        private async Task<LeaseResponse> ExecuteCreationRent(DeliveryMan deliveryMan, DateTime expectedEndDate)
        {
            //todo: fazer transaction para criação da locação

            var rent = new Rent(deliveryMan, expectedEndDate);

            rent.CalculateRentalTotalCost();


        }

        private async void SetMotorcycleOwner(Motorcycle motorcycle, DeliveryMan deliveryMan)
        {
            _logger.LogInformation("Relationship deliveryman and his motorcycle flow starting...");

            deliveryMan.SetMotorcycle(motorcycle);

            await _deliverymanRepository.UnitOfWork.PersistChangesAsync();
        }
    }
}
