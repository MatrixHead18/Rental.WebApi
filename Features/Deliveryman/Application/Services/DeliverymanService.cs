using Rental.WebApi.Features.Administrator.Application.Services;
using Rental.WebApi.Features.Deliveryman.Adapters.Repositories;
using Rental.WebApi.Features.Deliveryman.Application.Interfaces;
using Rental.WebApi.Features.Deliveryman.Application.Models.Requests;
using Rental.WebApi.Features.Deliveryman.Domain.Entities;
using Rental.WebApi.Features.Deliveryman.Domain.Enums;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

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

            string fileExtension = ValidateImageFormat(request.CNHImage);

            string filePath = SaveCNHImageLocally(request.CNHImage, request.CNHNumber, fileExtension);

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

            string fileExtension = ValidateImageFormat(request.CNHImage);

            deliveryMan.UpdateCnhImage(request.CNHImage);

            await _deliveryManRepository.UpdateOneAsync(deliveryMan, cancellationToken);
        }

        private string ValidateImageFormat(byte[] imageBytes)
        {
            using var memoryStream = new MemoryStream(imageBytes);
            using var image = Image.FromStream(memoryStream);

            return image.RawFormat switch
            {
                var format when format.Equals(ImageFormat.Png) => ".png",
                var format when format.Equals(ImageFormat.Bmp) => ".bmp",
                _ => throw new Exception("Invalid image format. Only PNG and BMP are supported.")
            };
        }

        private string SaveCNHImageLocally(byte[] imageBytes, string cnhNumber, string fileExtension)
        {
            var fileName = $"{cnhNumber}_CNH{fileExtension}";
            var fullPath = Path.Combine(_storagePath, fileName);

            File.WriteAllBytes(fullPath, imageBytes);

            return fullPath;
        }
    }
}
