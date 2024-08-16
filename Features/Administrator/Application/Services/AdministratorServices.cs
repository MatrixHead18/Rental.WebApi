using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Rental.WebApi.Features.Administrator.Application.Interfaces;
using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Administrator.Application.Models.Responses;
using Rental.WebApi.Features.Administrator.Domain.Events.ModelEvents;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.Domain.Exceptions;

namespace Rental.WebApi.Features.Administrator.Application.Services
{
    public class AdministratorServices : IAdministratorServices
    {
        private readonly ILogger<AdministratorServices> _logger;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IBus _busService;

        public AdministratorServices(
            ILogger<AdministratorServices> logger, 
            IMotorcycleRepository motorcycleRepository,
            IBus busService
        )
        {
            _logger = logger;
            _motorcycleRepository = motorcycleRepository;
            _busService = busService;
        }

        public async Task CreateMotorcycleAsync(CreateNewMotorcycleRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending message event to create motorcycle...");

            var createMotorcycleEvent = new MotorcycleCreatedEvent(request.Year, request.Model, request.LicensePlate);

            await _busService.PubSub.PublishAsync(createMotorcycleEvent);

            _logger.LogInformation("Message event sent successfull.");
        }

        public async Task DeleteMotorcycleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting motorcycle from database...");

            var motorcycle = await _motorcycleRepository.FindByIdAsync(
                filter: f=> f.Id == id, 
                includes: (x) => x.Include(g=> g.Lease),
                cancellationToken: cancellationToken);

            if (motorcycle is null)
            {
                _logger.LogWarning("There's no motorcycle founded.");

                throw new InvalidOperationException();
            }

            if (motorcycle.Lease is not null || motorcycle.Lease!.IsActive)
            {
                _logger.LogWarning("Motorcycle has a lease active, not possible delete de motorcycle.");

                throw new DomainException($"Lease to the motorcycle with id: {motorcycle.Id}, has active");
            }

            await _motorcycleRepository.DeleteOneAsync(motorcycle);
        }

        public async Task<MotorcycleResponse> GetMotorcycleByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var motorcycle = await _motorcycleRepository.FindByIdAsync(
                 filter: f => f.Id == id,
                 includes: (x) => x.Include(g => g.Lease),
                 cancellationToken: cancellationToken);

            if (motorcycle is null)
            {
                _logger.LogWarning("There's no motorcycle in database registered.");

                throw new InvalidOperationException();
            }

            return new MotorcycleResponse
            {
                Id = motorcycle.Id,
                LicensePlate = motorcycle.LicensePlate,
                Model = motorcycle.Model,
                Year = motorcycle.Year
            };
        }

        public async Task<IEnumerable<MotorcycleResponse>> GetAllMotorcyclesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting motorcycles...");

            var result = await _motorcycleRepository.GetAllAsync(x=> true, cancellationToken: cancellationToken);

            return result.Select(x => new MotorcycleResponse
            {
                Id = x.Id,
                LicensePlate = x.LicensePlate,
                Model = x.Model,
                Year = x.Year
            });
        }

        public async Task UpdateMotorcycleAsync(UpdateMotorcycleRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating motorcycle with id: {id}...", request.Id);

            var motorcycle = await _motorcycleRepository.FindByIdAsync(f=> f.Id == request.Id, cancellationToken: cancellationToken);

            if (motorcycle is null) 
            {
                _logger.LogWarning("There's no motorcycle in database registered.");

                throw new InvalidOperationException();
            }

            motorcycle.UpdateMotorcycle(request.LicensePlate);

            await _motorcycleRepository.UpdateOneAsync(motorcycle);
        }
    }
}
