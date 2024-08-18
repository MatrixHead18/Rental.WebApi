namespace Rental.WebApi.Features.Lease.Application.Models.Responses
{
    public class LeaseResponse
    {
        public Guid IdLease { get; set; }

        public string ModelMotorcycle { get; set; } = string.Empty;
        public string LicensePlateMotorcycle { get; set; } = string.Empty;

        public string NameDeliveryman { get; set; } = string.Empty;
        public string CpfDeliveryman { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public DateTime LeaseInitialDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
    }
}
