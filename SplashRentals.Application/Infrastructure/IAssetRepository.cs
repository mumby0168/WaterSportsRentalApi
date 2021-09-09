using System.Threading.Tasks;
using SplashRentals.Domain.Entities;

namespace SplashRentals.Application.Infrastructure
{
    public interface IAssetRepository
    {
        ValueTask<bool> AssetTypeExistsAsync(string id);
        ValueTask<bool> ExistsAsync(string win);
        ValueTask CreateAsync(Asset asset);
    }
}