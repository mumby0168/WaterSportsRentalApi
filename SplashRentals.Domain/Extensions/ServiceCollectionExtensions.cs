using Microsoft.Extensions.DependencyInjection;

namespace SplashRentals.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSplashRentalsDomain(this IServiceCollection services) => services;
    }
}