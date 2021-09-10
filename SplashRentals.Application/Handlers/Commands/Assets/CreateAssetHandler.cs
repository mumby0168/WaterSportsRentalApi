using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SplashRentals.Application.Commands.Assets;
using SplashRentals.Application.Infrastructure;
using SplashRentals.Domain.Entities;
using static SplashRentals.Application.Exceptions.SplashRentalsExceptions;

namespace SplashRentals.Application.Handlers.Commands.Assets
{
    class CreateAssetHandler : IRequestHandler<CreateAsset>
    {
        private readonly ILogger<CreateAssetHandler> _logger;
        private readonly IAssetRepository _assetRepository;

        public CreateAssetHandler(ILogger<CreateAssetHandler> logger, IAssetRepository assetRepository)
        {
            _logger = logger;
            _assetRepository = assetRepository;
        }
        
        public async Task<Unit> Handle(CreateAsset request, CancellationToken cancellationToken)
        {
            if (await _assetRepository.ExistsAsync(request.Win))
            {
                _logger.LogError($"Asset already created with win {request.Win}.");
                AssetAlreadyExists(request.Win);
            }

            if (await _assetRepository.AssetTypeExistsAsync(request.AssetType.Id) is false)
            {
                _logger.LogError($"Asset type {request.AssetType.Name} not found with id {request.AssetType.Id}.");
                AssetTypeNotFound(request.AssetType.Id);
            }

            Asset asset = new(request.Win, new AssetType(request.AssetType.Name, request.AssetType.Id), request.Occupancy);

            await _assetRepository.CreateAsync(asset);
            
            _logger.LogInformation($"Asset created with win {request.Win} and type {asset.AssetType.Name}.");

            return Unit.Value;
        }
    }
}