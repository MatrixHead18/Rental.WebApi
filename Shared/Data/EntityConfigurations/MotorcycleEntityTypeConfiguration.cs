using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Administrator.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class MotorcycleEntityTypeConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.Property(p => p.Year)
                .HasColumnName("Year")
                .IsRequired();

            builder.Property(p => p.Model)
                .HasColumnName("Model")
                .IsRequired();

            builder.Property(p => p.LicensePlate)
                .HasColumnName("LicensePlate")
                .IsRequired();

            builder
                .HasKey(v => v.Id);

            builder.ToCollection("Motorcycles");
        }
    }
}
