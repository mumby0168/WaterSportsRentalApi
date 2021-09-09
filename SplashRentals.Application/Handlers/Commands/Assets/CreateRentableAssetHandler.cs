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
    class CreateRentableAssetHandler : IRequestHandler<CreateAsset>
    {
        private readonly ILogger<CreateRentableAssetHandler> _logger;
        private readonly IAssetRepository _assetRepository;

        public CreateRentableAssetHandler(ILogger<CreateRentableAssetHandler> logger, IAssetRepository assetRepository)
        {
            _logger = logger;
            _assetRepository = assetRepository;
        }
        
        public async Task<Unit> Handle(CreateAsset request, CancellationToken cancellationToken)
        {
            if(await _assetRepository.ExistsAsync(request.Win))
                AssetAlreadyExists(request.Win);
            
            if (await _assetRepository.AssetTypeExistsAsync(request.AssetType.Id) is false)
                AssetTypeNotFound(request.AssetType.Id);

            Asset asset = new(request.Win, new AssetType(request.AssetType.Name, request.AssetType.Id), request.Occupancy);

            await _assetRepository.CreateAsync(asset);

            return Unit.Value;
        }
    }
}