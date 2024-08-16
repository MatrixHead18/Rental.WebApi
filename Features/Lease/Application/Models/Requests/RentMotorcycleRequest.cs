namespace Rental.WebApi.Features.Lease.Application.Models.Requests
{
    public class RentMotorcycleRequest
    {
        public Guid? IdDeliveryMan { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
    }
}
