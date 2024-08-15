using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Shared.Data;
using Rental.WebApi.Shared.Data.Repositories;
using System.Linq.Expressions;

namespace Rental.WebApi.Features.Deliveryman.Adapters.Repositories
{
    public class DeliverymanRepository : RepositoryBase<DeliveryMan>, IDeliverymanRepository
    {
        private readonly DatabaseContext _databaseContext;

        public DeliverymanRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> ExistsByCNHNumberAsync(string cnhNumber)
        {
            Expression<Func<DeliveryMan, bool>>? filter = x => x.CNHNumber == cnhNumber;

            return await AnyAsync(filter);
        }

        public async Task<bool> ExistsByCPFAsync(string cpf)
        {
            Expression<Func<DeliveryMan, bool>>? filter = x => x.CPF.Numero == cpf;

            return await AnyAsync(filter);
        }
    }
}
