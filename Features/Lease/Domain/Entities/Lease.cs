using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Domain.Objects;

namespace Rental.WebApi.Features.Lease.Domain.Entities
{
    public class Lease : Entity, IAggregateRoot
    {
        public Lease(DeliveryMan deliveryMan, LeasePlan leasePlan)
        {
            if (deliveryMan.CNHType != (CNHType.A | CNHType.B | CNHType.AB))
            {
                throw new DomainException("Only delivery drivers qualified in category A can make a rental.");
            }

            IsActive = true;
            Deliveryman = deliveryMan;
            LeasePlan = leasePlan;
            CreationDate = DateTime.Today;
            InitialDate = CreationDate.AddDays(1);
            ExpectedEndDate = InitialDate.AddDays(LeasePlan.DurationDays - 1);
            EndDate = ExpectedEndDate;
            TotalCost = LeasePlan.CalculateTotalCost();
        }

        public bool IsActive { get; private set; } = false;

        public DateTime CreationDate { get; private set; }

        public DateTime InitialDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime ExpectedEndDate { get; private set; }

        public decimal TotalCost { get; private set; }

        public Guid MotorCycleId { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }

        public Guid LeasePlanId { get; set; }
        public virtual LeasePlan LeasePlan { get; set; }

        public Guid DeliverymanId { get; set; }
        public virtual DeliveryMan Deliveryman { get; set; }

        public decimal CalculateLeaseTotalValue(DateTime devolutionDate)
        {
            if (devolutionDate < InitialDate)
                throw new DomainException("The return date cannot be earlier than the rental start date.");

            if (devolutionDate <= ExpectedEndDate)
                return CalculateValueFineBeetween7And15Days(devolutionDate);
            
            return CalculateValueFineSuperiorExpectedDate(devolutionDate);
        }

        private decimal CalculateValueFineSuperiorExpectedDate(DateTime devolutionDate)
        {
            int additionalDays = (devolutionDate - ExpectedEndDate).Days;
            decimal additionalDaysCost = additionalDays * 50.00m;
            return LeasePlan.CalculateTotalCost() + additionalDaysCost;
        }

        private decimal CalculateValueFineBeetween7And15Days(DateTime devolutionDate)
        {
            int daysNotEffective;
            decimal valueFine;

            daysNotEffective = (ExpectedEndDate - devolutionDate).Days;
            valueFine = 0;

            if (LeasePlan.DurationDays == 7)
                valueFine = daysNotEffective * LeasePlan.CostPerDay * 0.20m;

            else if (LeasePlan.DurationDays == 15)
                valueFine = daysNotEffective * LeasePlan.CostPerDay * 0.40m;

            return LeasePlan.CalculateTotalCost() - daysNotEffective * LeasePlan.CostPerDay + valueFine;
        }
    }
}
