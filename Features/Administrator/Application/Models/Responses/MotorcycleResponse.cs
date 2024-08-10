namespace Rental.WebApi.Features.Administrator.Application.Models.Responses
{
    public class MotorcycleResponse
    {
        public Guid Id { get; set; }
        public DateOnly Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
    }
}
