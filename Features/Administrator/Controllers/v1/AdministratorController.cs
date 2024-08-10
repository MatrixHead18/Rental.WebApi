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
        public async Task<IActionResult> InsertNewMotorcycleAsync([FromBody, Required]CreateNewMotorcycleRequest request)
        {
            try
            {
                await _administratorServices.CreateMotorcycleAsync(request);

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

        [HttpGet("/motorcycles")]
        public async Task<IActionResult> GetMotorcyclesAsync()
        {
            try
            {
                var result = await _administratorServices.GetAllMotorcyclesAsync();

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while getting motorcycles",
                        Details = ex.Message
                    }
                );
            }
        }

        [HttpPut("/update-motorcycle")]
        public async Task<IActionResult> UpdateMotorcycleAsync([FromBody, Required] UpdateMotorcycleRequest request)
        {
            try
            {
                await _administratorServices.UpdateMotorcycleAsync(request);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while updating motorcycle from request: {request}",
                        Details = ex.Message
                    }
                );
            }
        }

        [HttpGet("/motorcycle/{id:string}")]
        public async Task<IActionResult> GetMotorcycleByIdAsync([FromQuery, Required] string id)
        {
            try
            {
                var result = await _administratorServices.GetMotorcycleByIdAsync(id);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while getting motorcycle by id: {id}",
                        Details = ex.Message
                    }
                );
            }
        }

        [HttpGet("/delete-motorcycle/{id:string}")]
        public async Task<IActionResult> DeleteMotorcycleAsync([FromQuery, Required] string id)
        {
            try
            {
                await _administratorServices.DeleteMotorcycleAsync(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        StatusCode = 400,
                        Message = $"Error while deleting motorcycle by id: {id}",
                        Details = ex.Message
                    }
                );
            }
        }
    }
}
