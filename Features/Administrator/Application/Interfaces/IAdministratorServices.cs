using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Administrator.Application.Models.Responses;

namespace Rental.WebApi.Features.Administrator.Application.Interfaces
{
    public interface IAdministratorServices
    {
        Task CreateMotorcycleAsync(CreateNewMotorcycleRequest model, CancellationToken cancellationToken = default);
        Task<IEnumerable<MotorcycleResponse>> GetMotorcyclesAsync(CancellationToken cancellationToken = default);
    }
}
