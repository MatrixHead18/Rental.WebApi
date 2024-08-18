using Microsoft.AspNetCore.Mvc;
using Rental.WebApi.Features.Lease.Application.Interfaces;
using Rental.WebApi.Features.Lease.Application.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Rental.WebApi.Features.Lease.Controllers.v1
{
    [ApiVersion("1.0")]
    [ControllerName("Lease")]
    [Route("v{version: apiVersion}/{controller}")]
    public class LeaseController : ControllerBase
    {
        private readonly ILeaseServices _leaseServices;

        public LeaseController(ILeaseServices leaseServices)
        {
            _leaseServices = leaseServices;
        }

        /// <summary>
        /// Rent a motorcycle
        /// </summary>
        /// <param name="request.IdDeliveryMan">Deliveryman code</param>
        /// <param name="request.LicensePlate">Motorcycle license plate to rent</param>
        /// <param name="request.InitialDate">Initial date to rent</param>
        /// <param name="request.ExpectedEndDate">Expected date to rent</param>
        /// <returns>Rental object</returns>
        [HttpPost("/rent-motorcycle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateLease([FromBody, Required] RentMotorcycleRequest request)
        {
            try
            {
                var result = await _leaseServices.RentAMotorcycleAsync(request);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Error while creating a new lease.",
                        Details = ex.Message
                    }
                );
            }
        }

        /// <summary>
        /// Rent a motorcycle with end date customized
        /// </summary>
        /// <param name="request.RentalId">Deliveryman code</param>
        /// <param name="request.ExpectedEndDate">Expected date to rent</param>
        /// <returns>Rental object calculated with fines</returns>
        [HttpPost("/calculate-rent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CalculateLeaseFromExpectedDate([FromBody, Required] UpdateLeaseCostRequest request)
        {
            try
            {
                var result = await _leaseServices.CalculateTotalRent(request.RentalId, request.ExpectedEndDate);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Error while creating a new lease.",
                        Details = ex.Message
                    }
                );
            }
        }
    }
}
