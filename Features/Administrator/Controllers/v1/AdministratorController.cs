using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.WebApi.Features.Administrator.Application.Interfaces;
using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Rental.WebApi.Features.Administrator.Controllers.v1
{
    [ApiVersion("1.0")]
    [ControllerName("Administrator")]
    [Route("v{version: apiVersion}/{controller}")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorServices _administratorServices;

        public AdministratorController(IAdministratorServices administratorServices)
        {
            _administratorServices = administratorServices;
        }

        [HttpPost("/create-motorcycle")]
        public async Task<IActionResult> InsertNewMotorcycleAsync([FromBody, Required]CreateNewMotorcycleRequest motorcycleModel)
        {
            try
            {
                await _administratorServices.CreateMotorcycleAsync(motorcycleModel);

                return new CreatedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new{ 
                        StatusCode = HttpStatusCode.BadRequest, 
                        Message = $"Error while creating a new motorcycle in database", 
                        Details = ex.Message 
                    }
                );
            }
        }

        [HttpGet("/get-motorcycles")]
        public async Task<IActionResult> GetMotorcyclesAsync()
        {
            try
            {
                await _administratorServices.GetMotorcyclesAsync();

                return new CreatedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while creating a new motorcycle",
                        Details = ex.Message
                    }
                );
            }
        }
    }
}
