using Rental.WebApi.Features.Lease.Application.Models.Requests;
using Rental.WebApi.Features.Lease.Application.Models.Responses;

namespace Rental.WebApi.Features.Lease.Application.Interfaces
{
    public interface ILeaseServices
    {
        Task<LeaseResponse> RentAMotorcycleAsync(RentMotorcycleRequest request, CancellationToken cancellationToken = default);
    }
}
