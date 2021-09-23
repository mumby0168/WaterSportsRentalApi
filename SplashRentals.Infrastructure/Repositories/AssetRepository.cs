using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using SplashRentals.Application.Infrastructure;
using SplashRentals.Domain.Entities;

namespace SplashRentals.Infrastructure.Repositories
{
    internal class AssetRepository : IAssetRepository
    {
        private readonly IRepository<AssetType> _assetTypeRepository;
        private readonly IRepository<Asset> _rentableAssetRepository;

        public AssetRepository(IRepository<AssetType> assetTypeRepository, IRepository<Asset> rentableAssetRepository)
        {
            _assetTypeRepository = assetTypeRepository;
            _rentableAssetRepository = rentableAssetRepository;
        }
        
        public async ValueTask<bool> AssetTypeExistsAsync(string id)
        {
            try
            {
                await _assetTypeRepository.GetAsync(id);
                return true;
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            
        }
        
        public async ValueTask<bool> ExistsAsync(string win)
        {
            try
            {
                await _rentableAssetRepository.GetAsync(win);
                return true;
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public async ValueTask CreateAsync(Asset asset)
        {
            await _assetTypeRepository.UpdateAsync(asset.AssetType);
            await _rentableAssetRepository.CreateAsync(asset);
        }
    }
}