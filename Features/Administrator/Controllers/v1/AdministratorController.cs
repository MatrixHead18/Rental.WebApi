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

        /// <summary>
        /// Insert a new motorcycle
        /// </summary>
        /// <param name="request.Year">Motorcycle year</param>
        /// <param name="request.Model">Motorcycle model</param>
        /// <param name="request.LicensePlate">Motorcycle license plate</param>
        [HttpPost("/create-motorcycle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Get all motorcycles
        /// </summary>
        /// <returns>List of all motorcycles registered in database</returns>
        [HttpGet("/motorcycles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Update license plate from motorcycle
        /// </summary>
        /// <param name="request.IdMotorcycle">Motorcycle identification</param>
        /// <param name="request.LicensePlate">Motorcycle license plate</param>
        [HttpPatch("/update-motorcycle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Get motorcycle by id
        /// </summary>
        /// <param name="id">Motorcycle identification</param>
        /// <returns>Motorcycle founded in database</returns>
        [HttpGet("/motorcycle/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetMotorcycleByIdAsync([FromQuery, Required] Guid id)
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

        /// <summary>
        /// Delete motorcycle from database
        /// </summary>
        /// <param name="id">Motorcycle identification</param>
        [HttpGet("/delete-motorcycle/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteMotorcycleAsync([FromQuery, Required] Guid id)
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
