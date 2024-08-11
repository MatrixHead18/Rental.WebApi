using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class LeaseEntityTypeConfiguration : IEntityTypeConfiguration<Lease>
    {
        public void Configure(EntityTypeBuilder<Lease> builder)
        {
            builder.Property(p => p.InitialDate)
                .HasColumnName("InitialDate")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            builder.Property(p => p.CreationDate)
                .HasColumnName("CreationDate")
                .IsRequired();

            builder.Property(p => p.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();

            builder.Property(p => p.ExpectedEndDate)
                .HasColumnName("ExpectedEndDate")
                .IsRequired();

            builder.Property(p => p.TotalCost)
                .HasColumnName("TotalCost")
                .IsRequired();

            builder.HasOne(c => c.LeasePlan)
               .WithOne(c => c.Lease)
               .HasForeignKey("LeasePlanId");

            builder.HasOne(c => c.Motorcycle)
               .WithOne(c => c.Lease)
               .HasForeignKey("MotorcycleId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("Motorcycles");
        }
    }
}
