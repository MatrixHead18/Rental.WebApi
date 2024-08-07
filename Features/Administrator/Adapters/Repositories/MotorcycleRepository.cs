using MongoDB.Driver;
using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.Data.Repositories;

namespace Rental.WebApi.Features.Administrator.Adapters.Repositories
{
    public class MotorcycleRepository : RepositoryBase<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(IMongoDatabase database) : base(database) { }

        public Task UpdateMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken)
        {
            var fieldToUpdate = Builders<Motorcycle>.Update
                .Set(doc => doc.LicensePlate, motorcycle.LicensePlate);

            return UpdateOneAsync(
                doc => doc.Id == motorcycle.Id,
                fieldToUpdate,
                new UpdateOptions { IsUpsert = true },
                cancellationToken: cancellationToken
            );
        }
    }
}
