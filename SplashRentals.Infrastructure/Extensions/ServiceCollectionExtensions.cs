using Microsoft.Extensions.DependencyInjection;
using SplashRentals.Application.Infrastructure;
using SplashRentals.Domain.Entities;
using SplashRentals.Infrastructure.Repositories;

namespace SplashRentals.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSplashRentalsInfrastructure(this IServiceCollection services) =>
            services
                .AddSingleton<IAssetRepository, AssetRepository>()
                .AddCosmosRepository(options =>
                {
                    options.DatabaseId = "splash-rentals-db";
                    options.ContainerId = "splash-rentals";
                });
    }
}