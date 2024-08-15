using Microsoft.AspNetCore.Mvc;
using Rental.WebApi.Features.Deliveryman.Application.Interfaces;
using Rental.WebApi.Features.Deliveryman.Application.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Rental.WebApi.Features.Deliveryman.Controllers.v1
{
    [ApiVersion("1.0")]
    [ControllerName("Deliveryman")]
    [Route("v{version: apiVersion}/{controller}")]
    public class DeliverymanController : ControllerBase
    {
        private readonly IDeliverymanService _deliveryManService;
        public DeliverymanController(IDeliverymanService deliverymanService)
        {
            _deliveryManService = deliverymanService;
        }

        [HttpPost("/create-deliveryman")]
        public async Task<IActionResult> CreateNewDeliveryMan([FromBody, Required] CreateNewDeliveryManRequest request)
        {
            try
            {
                await _deliveryManService.CreateDeliveryManAsync(request);

                return new CreatedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Error while creating a new delivery man",
                        Details = ex.Message
                    }
                );
            }
        }

        [HttpPut("/update-deliveryman")]
        public async Task<IActionResult> UpdateDeliveryMan([FromBody, Required] UpdateDeliveryManRequest request)
        {
            try
            {
                await _deliveryManService.UpdateDeliveryManAsync(request);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while updating deliveryman from request: {request}",
                        Details = ex.Message
                    }
                );
            }
        }
    }
}
