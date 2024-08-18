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

        /// <summary>
        /// Insert a new deliveryman
        /// </summary>
        /// <param name="request.Name">Deliveryman name</param>
        /// <param name="request.CPF">Deliveryman CPF</param>
        /// <param name="request.BirthDate">Deliveryman birth date</param>
        /// <param name="request.CNHNumber">Deliveryman cnh number</param>
        /// <param name="request.CNHType">Deliveryman cnh type</param>
        /// <param name="request.CNHImage">Deliveryman cnh image</param>
        [HttpPost("/create-deliveryman")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Insert a new deliveryman
        /// </summary>
        /// <param name="request.DeliverymanId">Deliveryman identification</param>
        /// <param name="request.CNHImage">Deliveryman cnh image</param>
        [HttpPatch("/update-deliveryman")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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
