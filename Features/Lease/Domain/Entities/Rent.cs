using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Domain.Objects;

namespace Rental.WebApi.Features.Lease.Domain.Entities
{
    public class Rent : Entity, IAggregateRoot
    {
        public Rent(DeliveryMan deliveryMan, DateTime devolutionDate)
        {
            if (deliveryMan.CNHType != (CNHType.A | CNHType.AB))
                throw new DomainException("Only delivery drivers qualified in category A or AB can make a rental.");

            IsActive = true;

            Deliveryman = deliveryMan;
            DeliverymanId = deliveryMan.Id;
            MotorcycleId = Deliveryman.MotorcycleId;
            Motorcycle = Deliveryman.Motorcycle;

            CreationDate = DateTime.Today;
            InitialDate = CreationDate.AddDays(1);
            RentPlan = new RentPlan(CalculateDurationDays(devolutionDate));
            ExpectedEndDate = InitialDate.AddDays(RentPlan.DurationDays - 1);
            EndDate = ExpectedEndDate;
        }

        public bool IsActive { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime InitialDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime ExpectedEndDate { get; private set; }

        public decimal TotalCost { get; private set; }

        public Guid? MotorcycleId { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }

        public Guid RentPlanId { get; set; }
        public virtual RentPlan RentPlan { get; set; }

        public Guid DeliverymanId { get; set; }
        public virtual DeliveryMan Deliveryman { get; set; }

        public void CalculateRentalTotalCost()
        {
            TotalCost = RentPlan.CalculateTotalCost();
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

            TotalCost = RentPlan.CalculateTotalCost() + additionalDaysCost;

            return TotalCost;
        }

        private decimal CalculateValueFineBeetween7And15Days(DateTime devolutionDate)
        {
            int daysNotEffective;
            decimal valueFine;

            daysNotEffective = (ExpectedEndDate - devolutionDate).Days;
            valueFine = 0;

            if (RentPlan.DurationDays == 7)
                valueFine = (daysNotEffective * RentPlan.CostPerDay) * 0.20m;

            else if (RentPlan.DurationDays == 15)
                valueFine = (daysNotEffective * RentPlan.CostPerDay) * 0.40m;

            TotalCost = RentPlan.CalculateTotalCost() - (daysNotEffective * RentPlan.CostPerDay) + valueFine;

            return TotalCost;
        }
    }
}
