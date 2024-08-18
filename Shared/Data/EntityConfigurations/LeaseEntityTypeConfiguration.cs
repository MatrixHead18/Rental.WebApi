using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Lease.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class LeaseEntityTypeConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.Property(p => p.InitialDate)
                .HasColumnName("InitialDate")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            builder.Property(p => p.InitialDate)
                .HasColumnName("InitialDate")
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

            builder.HasOne(c => c.RentalPlan)
               .WithOne(c => c.Rent)
               .HasForeignKey("LeasePlanId");

            builder.HasOne(c => c.Deliveryman)
               .WithOne(c => c.Rental)
               .HasForeignKey("RentalId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("Leases");
        }
    }
}
