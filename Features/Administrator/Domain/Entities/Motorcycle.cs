using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Features.Administrator.Domain.Entities
{
    public class Motorcycle : EntityModel
    {
        public Motorcycle(DateOnly year, string model, string licensePlate)
        {
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }

        public DateOnly Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;

        public override bool Equals(object? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return other is Motorcycle motorcycle &&
                   LicensePlate == motorcycle.LicensePlate;
        }

        public override int GetHashCode()
            => HashCode.Combine(LicensePlate, Model);
    }
}
