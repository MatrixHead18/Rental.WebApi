using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;

namespace Rental.WebApi.Shared.Data.EntityConfigurations
{
    public class LeasePlanEntityTypeConfiguration : IEntityTypeConfiguration<LeasePlan>
    {
        public void Configure(EntityTypeBuilder<LeasePlan> builder)
        {
            builder.Property(p => p.DurationDays)
                .HasColumnName("DurationDays")
                .IsRequired();

            builder.Property(p => p.CostPerDay)
                .HasColumnName("CostPerDay")
                .IsRequired();

            builder.HasOne(c => c.Lease)
               .WithOne(c => c.LeasePlan)
               .HasForeignKey("LeaseId");

            builder
                .HasIndex(v => v.Id).IsUnique();

            builder.ToCollection("Motorcycles");
        }
    }
}
