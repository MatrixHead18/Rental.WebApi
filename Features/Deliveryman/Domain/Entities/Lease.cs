using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Features.Deliveryman.Domain.Entities
{
    public class Lease : EntityModel
    {
        public Lease(DateTime endDate, DateTime expectedEndDate, DeliveryMan deliveryman)
        {
            if (deliveryman.CNHType != (CNHType.A | CNHType.B | CNHType.AB) )
            {
                throw new InvalidOperationException("Somente entregadores habilitados na categoria A podem efetuar uma locação.");
            }

            CreationDate = DateTime.Today;
            InitialDate = CreationDate.AddDays(1);
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
        }
        public DateTime CreationDate { get; private set; }

        public DateTime InitialDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateTime ExpectedEndDate { get; private set; }


        public virtual Guid? DeliverymanId { get; set; }
        public virtual DeliveryMan Deliveryman{ get; set; }

        public TimeSpan CalculateDuration()
        {
            return EndDate.Subtract(InitialDate);
        }

        public bool ThisWithinDeadline()
        {
            return EndDate <= ExpectedEndDate;
        }
    }
}
