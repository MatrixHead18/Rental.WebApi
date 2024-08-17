using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Lease.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class LeasePlanEntityTypeConfiguration : IEntityTypeConfiguration<RentPlan>
    {
        public void Configure(EntityTypeBuilder<RentPlan> builder)
        {
            builder.Property(p => p.DurationDays)
                .HasColumnName("DurationDays")
                .IsRequired();

            builder.Property(p => p.CostPerDay)
                .HasColumnName("CostPerDay")
                .IsRequired();

            builder.HasOne(c => c.Rent)
               .WithOne(c => c.RentPlan)
               .HasForeignKey("LeaseId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("Motorcycles");
        }
    }
}
