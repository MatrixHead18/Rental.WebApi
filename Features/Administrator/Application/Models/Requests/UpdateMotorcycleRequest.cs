using System.ComponentModel.DataAnnotations;

namespace Rental.WebApi.Features.Administrator.Application.Models.Requests
{
    public class UpdateMotorcycleRequest
    {
        [Required(ErrorMessage = "The Id has incorrect value")]
        public Guid IdMotorcycle { get; set; }

        [Required(ErrorMessage = "The LicencePlate has incorrect value")]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
