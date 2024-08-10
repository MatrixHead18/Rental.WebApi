using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Features.Administrator.Domain.Events.ModelEvents
{
    public class MotorcycleCreatedEvent : DomainEvent
    {
        public MotorcycleCreatedEvent(DateOnly year, string model, string licensePlate)
        {
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }

        public DateOnly Year { get; private set; }
        public string Model { get; private set; } = string.Empty;
        public string LicensePlate { get; private set; } = string.Empty;
    }
}
