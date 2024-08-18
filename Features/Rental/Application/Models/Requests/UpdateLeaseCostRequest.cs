namespace Rental.WebApi.Features.Lease.Application.Models.Requests
{
    public class UpdateLeaseCostRequest
    {
        public Guid RentalId { get; set; }
        public DateTime ExpectedEndDate { get; set; }
    }
}