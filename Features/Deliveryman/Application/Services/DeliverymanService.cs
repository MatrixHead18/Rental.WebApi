using Rental.WebApi.Features.Deliveryman.Application.Interfaces;
using Rental.WebApi.Features.Deliveryman.Application.Models.Requests;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace Rental.WebApi.Features.Deliveryman.Application.Services
{
    public class DeliverymanService : IDeliverymanService
    {
        private readonly ILogger<DeliverymanService> _logger;
        private readonly IDeliverymanRepository _deliveryManRepository;
        private readonly string _storagePath = "C:\\Users\\Public";

        public DeliverymanService(IDeliverymanRepository deliveryManRepository,
                                  ILogger<DeliverymanService> logger)
        {
            _deliveryManRepository = deliveryManRepository;
            _logger = logger;
        }

        public async Task CreateDeliveryManAsync(CreateNewDeliveryManRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating a delivery man...");

            if (await _deliveryManRepository.ExistsByCPFAsync(request.CPF))
                throw new Exception("CPF already registered.");

            if (await _deliveryManRepository.ExistsByCNHNumberAsync(request.CNHNumber))
                throw new Exception("CNH number already registered.");

            SaveCNHImageLocally(request.CNHImage, request.CNHNumber);

            var deliveryMan = new DeliveryMan(request.Name, request.CPF, request.BirthDate, request.CNHNumber, request.CNHType, request.CNHImage);

            await _deliveryManRepository.InsertOneAsync(deliveryMan, cancellationToken);
        }


        public async Task UpdateDeliveryManAsync(UpdateDeliveryManRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating a delivery man...");

            var deliveryMan = await _deliveryManRepository.FindByIdAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

            if (deliveryMan is null)
            {
                _logger.LogWarning("There's no deliveryman in database registered.");
                throw new InvalidOperationException();
            }

            _ = ValidateImageFormat(request.CNHImage);

            deliveryMan.UpdateCnhImage(request.CNHImage);

            await _deliveryManRepository.UpdateOneAsync(deliveryMan, cancellationToken);
        }

        private string ValidateImageFormat(byte[] imageBytes)
        {
            using var image = Image.Load(imageBytes);

            IImageFormat format = image.Metadata.DecodedImageFormat;

            return format.Name switch
            {
                "PNG" => ".png",
                "BMP" => ".bmp",
                _ => throw new NotSupportedException("Invalid image format. Only PNG and BMP are supported.")
            };

        }

        private void SaveCNHImageLocally(byte[] imageBytes, string cnhNumber)
        {
            var fileExtension = ValidateImageFormat(imageBytes);

            var fileName = $"{cnhNumber}_CNH{fileExtension}";
            var fullPath = Path.Combine(_storagePath, fileName);

            File.WriteAllBytes(fullPath, imageBytes);
        }
    }
}
