using Microsoft.EntityFrameworkCore;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Features.Lease.Application.Interfaces;
using Rental.WebApi.Features.Lease.Application.Models.Requests;
using Rental.WebApi.Features.Lease.Application.Models.Responses;
using Rental.WebApi.Features.Lease.Domain.Entities;
using Rental.WebApi.Features.Lease.Domain.Interfaces;
using System.Data;

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

            return new LeaseResponse
            {
                IdLease = rental.Id,
                CpfDeliveryman = rental.Deliveryman.CPF.Numero,
                NameDeliveryman = rental.Deliveryman.Name,
                ModelMotorcycle = rental.Deliveryman.Motorcycle.Model,
                LicensePlateMotorcycle = rental.Deliveryman.Motorcycle.LicensePlate,
                IsActive = rental.IsActive,
                LeaseInitialDate = rental.InitialDate,
                LeaseEndDate = rental.EndDate
            };
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

                return new LeaseResponse { };
            }

            await SetMotorcycleOwner(motorcycle!, deliveryMan);

            var leaseResponse = await ExecuteCreationRentAsync(deliveryMan, request.ExpectedEndDate, cancellationToken);

            return leaseResponse;
        }

        private async Task<LeaseResponse> ExecuteCreationRentAsync(DeliveryMan deliveryMan, DateTime expectedEndDate, CancellationToken cancellationToken)
        {
            var executionStrategy = _rentalRepository.UnitOfWork.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                using var transaction = await _rentalRepository.UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

                try
                {
                    var rent = new Rent(deliveryMan, expectedEndDate);

                    rent.CalculateRentalTotalCost();

                    await _rentalRepository.UnitOfWork.CommitTransactionAsync(transaction, cancellationToken);

                    _logger.LogInformation("Rental created succesfully.");

                    return new LeaseResponse
                    {
                        IdLease = rent.Id,
                        CpfDeliveryman = deliveryMan.CPF.Numero,
                        NameDeliveryman = deliveryMan.Name,
                        ModelMotorcycle = deliveryMan.Motorcycle.Model,
                        LicensePlateMotorcycle = deliveryMan.Motorcycle.LicensePlate,
                        IsActive = rent.IsActive,
                        LeaseInitialDate = rent.InitialDate,
                        LeaseEndDate = rent.EndDate
                    };
                }
                catch
                {
                    await _rentalRepository.UnitOfWork.RollbackTransactionAsync(transaction, cancellationToken);
                    throw;
                }
            });

            return new LeaseResponse { };
        }

        private async Task SetMotorcycleOwner(Motorcycle motorcycle, DeliveryMan deliveryMan)
        {
            _logger.LogInformation("Relationship deliveryman and his motorcycle flow starting...");

            deliveryMan.SetMotorcycle(motorcycle);

            await _deliverymanRepository.UnitOfWork.PersistChangesAsync();
        }
    }
}
