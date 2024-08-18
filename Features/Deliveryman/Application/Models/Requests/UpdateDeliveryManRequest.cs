using System.ComponentModel.DataAnnotations;

namespace Rental.WebApi.Features.Deliveryman.Application.Models.Requests
{
    public class UpdateDeliveryManRequest
    {
        [Required(ErrorMessage = "The Id is empty")]
        public Guid DeliverymanId { get; set; }

        [Required(ErrorMessage = "The CNH image is empty")]
        public byte[] CNHImage { get; set; }
    }
}