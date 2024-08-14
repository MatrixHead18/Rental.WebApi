using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rental.WebApi.Features.Deliveryman.Application.Models.Requests
{
    public class CreateNewDeliveryManRequest
    {
        [Required(ErrorMessage = "The name is empty")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The CPF is empty")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "The birth date is incorrect")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "The CNH number is empty")]
        public string CNHNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "The CNH type is incorrect")]
        public int CNHType { get; set; }

        [Required(ErrorMessage = "The CNH image is empty")]
        public byte[] CNHImage { get; set; }
    }
}
