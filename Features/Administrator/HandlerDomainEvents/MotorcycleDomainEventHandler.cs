using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Events.ModelEvents;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.MessageBus.Interfaces;

namespace Rental.WebApi.Features.Administrator.HandlerDomainEvents
{
    public class MotorcycleDomainEventHandler : BackgroundService
    {
        private readonly IMessageBusService _bus;
        private readonly ILogger<MotorcycleDomainEventHandler> _logger;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleDomainEventHandler(
            IMessageBusService bus, 
            ILogger<MotorcycleDomainEventHandler> logger, 
            IMotorcycleRepository motorcycleRepository)
        {
            _bus = bus;
            _logger = logger;
            _motorcycleRepository = motorcycleRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber(stoppingToken);

            return Task.CompletedTask;
        }

        private void SetSubscriber(CancellationToken stoppingToken)
        {
            _bus.SubscribeAsync<MotorcycleCreatedEvent>(
                    "CreateMotorcycle", async request => await InsertMotorcycleAsync(request, stoppingToken));
        }

        private async Task InsertMotorcycleAsync(MotorcycleCreatedEvent request, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Inserting a new motorcycle in database...");

            var motorcycle = new Motorcycle(request.Year, request.Model, request.LicensePlate);

            await _motorcycleRepository.InsertOneAsync(motorcycle, stoppingToken);

            _logger.LogInformation("Create motorcycle successfull in database...");
        }
    }
}
