﻿using Rental.WebApi.Shared.Domain;

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

        public DateOnly Year { get; private set; }
        public string Model { get; private set; } = string.Empty;
        public string LicensePlate { get; private set; } = string.Empty;

        public override bool Equals(object? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return other is Motorcycle motorcycle &&
                   LicensePlate == motorcycle.LicensePlate;
        }

        public override int GetHashCode()
            => HashCode.Combine(LicensePlate, Model);

        public Motorcycle UpdateMotorcycle(string licensePlate)
        {
            LicensePlate = licensePlate;
            return this;
        }
    }
}
