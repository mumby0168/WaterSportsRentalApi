using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SplashRentals.Application.Extensions;
using SplashRentals.Domain.Extensions;
using SplashRentals.Infrastructure.Extensions;

namespace SplashRentals.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddSplashRentalsDomain()
                .AddSplashRentalsApplication()
                .AddSplashRentalsInfrastructure();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}