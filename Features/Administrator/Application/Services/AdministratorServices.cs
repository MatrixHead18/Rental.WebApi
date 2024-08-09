using Rental.WebApi.Features.Administrator.Application.Interfaces;
using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;

namespace Rental.WebApi.Features.Administrator.Application.Services
{
    public class AdministratorServices : IAdministratorServices
    {
        private readonly ILogger<AdministratorServices> _logger;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public AdministratorServices(
            ILogger<AdministratorServices> logger, 
            IMotorcycleRepository motorcycleRepository
        )
        {
            _logger = logger;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task CreateMotorcycleAsync(CreateNewMotorcycleRequest model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create motorcycle flow starting...");

            var motorcycle = new Motorcycle(model.Year, model.Model, model.LicensePlate);

            await _motorcycleRepository.InsertOneAsync(motorcycle, cancellationToken);

            _logger.LogInformation("Motorcycle created successfull.");
        }
    }
}
