using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Lease.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class DeliverymanEntityTypeConfiguration : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(p => p.CPF.Numero)
               .HasColumnName("CPF")
               .IsRequired();

            builder.Property(p => p.BirthDate)
                .HasColumnName("BirthDate")
                .IsRequired();

            builder.Property(p => p.CNHNumber)
                .HasColumnName("CNHNumber")
                .IsRequired();

            builder.Property(p => p.CNHImage)
                .HasColumnName("CNHImage")
                .IsRequired();

            builder.Property(p => p.CNHType)
                .HasColumnName("CNHType")
                .IsRequired();

            builder.HasOne(c => c.Motorcycle)
               .WithOne(c => c.DeliveryMan)
               .HasForeignKey("MotorcycleId");

            builder.HasOne(c => c.Rental)
                .WithOne(c => c.Deliveryman)
                .HasForeignKey("RentalId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("Deliverymans");
        }
    }
}
