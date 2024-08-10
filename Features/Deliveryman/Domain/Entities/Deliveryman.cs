using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Features.Deliveryman.Domain.Entities
{
    public class DeliveryMan : EntityModel
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; } 
        public string CNHNumber { get; set; } 
        public CNHType CNHType { get; set; } 
        public byte[] CNHImage { get; set; } 

        public DeliveryMan(string name, string cpf, DateTime birthDate, string cnhNumber, CNHType cnhType, byte[] cnhImage)
        {
            Name = name;
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
            BirthDate = birthDate;
            CNHNumber = cnhNumber ?? throw new ArgumentNullException(nameof(cnhNumber));
            CNHType = cnhType;
            CNHImage = cnhImage ?? throw new ArgumentNullException(nameof(cnhImage));
        }
    }
}
