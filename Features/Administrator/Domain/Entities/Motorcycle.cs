﻿using Rental.WebApi.Shared.Data.Attributes;
using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Features.Administrator.Domain.Entities
{
    [BsonCollection("Motorcyles")]
    public class Motorcycle : MongoDbDocument
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

        public Motorcycle UpdateMotorcycle(string licensePlate)
        {
            LicensePlate = licensePlate;
            return this;
        }
    }
}
