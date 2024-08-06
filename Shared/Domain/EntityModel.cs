using Rental.WebApi.Shared.Domain.Interfaces;

namespace Rental.WebApi.Shared.Domain
{
    public abstract class EntityModel : IEntity
    {
        public Guid Id { get; set; }
    }
}
