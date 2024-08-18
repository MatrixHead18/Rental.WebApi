using Microsoft.AspNetCore.Mvc;
using Rental.WebApi.Features.Administrator.Application.Models.Requests;
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

        [HttpPost("/rent-motorcycle")]
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

        [HttpPost("/calculate-rent")]
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
