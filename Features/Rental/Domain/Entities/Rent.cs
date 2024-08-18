using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Domain.Objects;

namespace Rental.WebApi.Features.Lease.Domain.Entities
{
    public class Rent : Entity, IAggregateRoot
    {
        public Rent(DeliveryMan deliveryMan, DateTime initialDate, DateTime devolutionDate)
        {
            if (deliveryMan.CNHType != (CNHType.A | CNHType.AB))
                throw new DomainException("Only delivery drivers qualified in category A or AB can make a rental.");

            IsActive = true;

            Deliveryman = deliveryMan;
            DeliverymanId = deliveryMan.Id;
            MotorcycleId = Deliveryman.MotorcycleId;
            Motorcycle = Deliveryman.Motorcycle;

            InitialDate = initialDate.AddDays(1);
            RentalPlan = new RentalPlan(CalculateDurationDays(devolutionDate));
            ExpectedEndDate = InitialDate.AddDays(RentalPlan.DurationDays - 1);
            EndDate = ExpectedEndDate;
        }

        public bool IsActive { get; private set; }

        public DateTime InitialDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime ExpectedEndDate { get; private set; }

        public decimal TotalCost { get; private set; }

        public Guid? MotorcycleId { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }

        public Guid RentalPlanId { get; set; }
        public virtual RentalPlan RentalPlan { get; set; }

        public Guid DeliverymanId { get; set; }
        public virtual DeliveryMan Deliveryman { get; set; }

        public void CalculateRentalTotalCost()
        {
            TotalCost = RentalPlan.CalculateTotalCost();
        }

        public decimal CalculateRentWithValueFineTotalValue(DateTime devolutionDate)
        {
            if (devolutionDate <= ExpectedEndDate)
                return CalculateValueFineBeetween7And15Days(devolutionDate);
            
            return CalculateValueFineSuperiorExpectedDate(devolutionDate);
        }

        private int CalculateDurationDays(DateTime devolutionDate) => devolutionDate.Subtract(InitialDate).Days;

        private decimal CalculateValueFineSuperiorExpectedDate(DateTime devolutionDate)
        {
            int additionalDays = (devolutionDate - ExpectedEndDate).Days;

            decimal additionalDaysCost = additionalDays * 50.00m;

            TotalCost = RentalPlan.CalculateTotalCost() + additionalDaysCost;

            return TotalCost;
        }

        private decimal CalculateValueFineBeetween7And15Days(DateTime devolutionDate)
        {
            int daysNotEffective;
            decimal valueFine;

            daysNotEffective = (ExpectedEndDate - devolutionDate).Days;
            valueFine = 0;

            if (RentalPlan.DurationDays == 7)
                valueFine = (daysNotEffective * RentalPlan.CostPerDay) * 0.20m;

            else if (RentalPlan.DurationDays == 15)
                valueFine = (daysNotEffective * RentalPlan.CostPerDay) * 0.40m;

            TotalCost = RentalPlan.CalculateTotalCost() - (daysNotEffective * RentalPlan.CostPerDay) + valueFine;

            return TotalCost;
        }
    }
}
