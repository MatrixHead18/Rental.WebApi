using Rental.WebApi.Features.Administrator.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain.Exceptions;
using Rental.WebApi.Shared.Domain.Objects;

namespace Rental.WebApi.Features.Deliveryman.Domain.Entities
{
    public class DeliveryMan : Entity
    {
        public string Name { get; set; }
        public Cpf CPF { get; set; }
        public DateTime BirthDate { get; set; } 
        public string CNHNumber { get; set; } 
        public CNHType CNHType { get; set; } 
        public byte[] CNHImage { get; set; }

        public Guid? MotorcycleId { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }

        public DeliveryMan(string name, string cpf, DateTime birthDate, string cnhNumber, int cnhType, byte[] cnhImage)
        {
            Name = name;
            CPF = new Cpf(cpf);
            BirthDate = birthDate;
            CNHNumber = cnhNumber ?? throw new DomainException($"Invalid field: {cnhNumber}");
            CNHType = (CNHType)cnhType;
            CNHImage = cnhImage ?? throw new DomainException($"Invalid field: {cnhImage}");
        }

        public void UpdateCnhImage(byte [] cnhImage)
        {
            CNHImage = cnhImage;
        }
    }
}
