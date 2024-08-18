using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Lease.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class RentalPlanEntityTypeConfiguration : IEntityTypeConfiguration<RentalPlan>
    {
        public void Configure(EntityTypeBuilder<RentalPlan> builder)
        {
            builder.Property(p => p.DurationDays)
                .HasColumnName("DurationDays")
                .IsRequired();

            builder.Property(p => p.CostPerDay)
                .HasColumnName("CostPerDay")
                .IsRequired();

            builder.HasOne(c => c.Rent)
               .WithOne(c => c.RentalPlan)
               .HasForeignKey("RentalPlanId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("RentalPlans");
        }
    }
}
