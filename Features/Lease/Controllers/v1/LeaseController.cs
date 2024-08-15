using Microsoft.AspNetCore.Mvc;
using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Lease.Application.Interfaces;
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

        [HttpPost("/create-lease")]
        public async Task<IActionResult> CreateLease([FromBody, Required] CreateNewMotorcycleRequest request)
        {
            try
            {
                await _leaseServices.CreateMotorcycleAsync(request);

                return new CreatedResult();
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
