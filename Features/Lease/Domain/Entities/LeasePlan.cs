using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Domain.Objects;

namespace Rental.WebApi.Features.Lease.Domain.Entities
{
    public class LeasePlan : Entity
    {
        public int DurationDays { get; private set; }
        public decimal CostPerDay { get; private set; }

        public LeasePlan(int durationDays)
        {
            DurationDays = durationDays;
            CostPerDay = DefineCostPerDay(durationDays);
        }

        public Guid? LeaseId { get; set; }
        public virtual Lease Lease { get; set; }

        private decimal DefineCostPerDay(int durationDays)
        {
            return durationDays switch
            {
                7 => 30.00m,
                15 => 28.00m,
                30 => 22.00m,
                45 => 20.00m,
                50 => 18.00m,
                _ => throw new DomainException("Not supported lease plan.")
            };
        }

        public decimal CalculateTotalCost()
        {
            return DurationDays * CostPerDay;
        }
    }
}
