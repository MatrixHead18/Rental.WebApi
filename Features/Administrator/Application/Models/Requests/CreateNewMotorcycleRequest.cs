using System.ComponentModel.DataAnnotations;

namespace Rental.WebApi.Features.Administrator.Application.Models.Requests
{
    public class CreateNewMotorcycleRequest
    {
        [Required(ErrorMessage = "The year of motorcycle has empty")]
        public DateOnly Year { get; set; }

        [Required(ErrorMessage = "The model has empty")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(7, ErrorMessage = "The license plate has error, check the information !")]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
