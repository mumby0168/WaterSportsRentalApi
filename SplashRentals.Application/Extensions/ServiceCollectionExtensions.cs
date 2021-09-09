using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SplashRentals.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSplashRentalsApplication(this IServiceCollection services) => 
            services.AddMediatR(typeof(ServiceCollectionExtensions));
    }
}