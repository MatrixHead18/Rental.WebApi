using MongoDB.Bson;

namespace Rental.WebApi.Features.Administrator.Application.Models.Responses
{
    public class MotorcycleResponse
    {
        public ObjectId Id { get; set; }
        public DateOnly Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
