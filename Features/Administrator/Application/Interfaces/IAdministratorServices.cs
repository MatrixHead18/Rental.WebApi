using Rental.WebApi.Features.Administrator.Application.Models.Requests;
using Rental.WebApi.Features.Administrator.Application.Models.Responses;

namespace Rental.WebApi.Features.Administrator.Application.Interfaces
{
    public interface IAdministratorServices
    {
        Task CreateMotorcycleAsync(CreateNewMotorcycleRequest model, CancellationToken cancellationToken = default);
        Task<IEnumerable<MotorcycleResponse>> GetAllMotorcyclesAsync(CancellationToken cancellationToken = default);
        Task<MotorcycleResponse> GetMotorcycleByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateMotorcycleAsync(UpdateMotorcycleRequest model, CancellationToken cancellationToken = default);
        Task DeleteMotorcycleAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
