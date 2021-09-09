using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SplashRentals.Application.Exceptions;
using SplashRentals.Application.Extensions;
using SplashRentals.Domain.Extensions;
using SplashRentals.Infrastructure.Extensions;
using SplashRentals.Web.Middleware;

namespace SplashRentals.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddMvcCore()
                .AddApiExplorer()
                .Services
                .AddSwaggerGen()
                .AddApiVersioning()
                .AddSingleton<SplashRentalsExceptionHandlingMiddleware>()
                .AddSplashRentalsDomain()
                .AddSplashRentalsApplication()
                .AddSplashRentalsInfrastructure();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app
                .UseMiddleware<SplashRentalsExceptionHandlingMiddleware>()
                .UseSwagger()
                .UseSwaggerUI()
                .UseRouting()
                .UseEndpoints(configure: endpoints =>
                {
                    endpoints.MapGet("/",  context =>
                    {
                        context.Response.Redirect("/swagger");
                        return Task.CompletedTask;
                    });
                    endpoints.MapControllers();
                });
    }
}