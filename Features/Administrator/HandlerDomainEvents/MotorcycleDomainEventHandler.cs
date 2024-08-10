using MediatR;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Events.ModelEvents;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.MessageBus.Interfaces;

namespace Rental.WebApi.Features.Administrator.HandlerDomainEvents
{
    public class MotorcycleDomainEventHandler : INotificationHandler<MotorcycleCreatedEvent>
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

        private async Task InsertMotorcycleAsync(MotorcycleCreatedEvent request, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Inserting a new motorcycle in database...");

            var motorcycle = new Motorcycle(request.Year, request.Model, request.LicensePlate);

            await _motorcycleRepository.InsertOneAsync(motorcycle, stoppingToken);

            _logger.LogInformation("Create motorcycle successfull in database...");
        }

        public Task Handle(MotorcycleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _bus.SubscribeAsync<MotorcycleCreatedEvent>(
                "CreateMotorcycle", async => InsertMotorcycleAsync(notification, cancellationToken));

            return Task.CompletedTask;
        }
    }
}
