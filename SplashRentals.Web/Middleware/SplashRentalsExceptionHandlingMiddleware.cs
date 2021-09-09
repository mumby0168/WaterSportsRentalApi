using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SplashRentals.Application.Exceptions;

namespace SplashRentals.Web.Middleware
{
    public class SplashRentalsExceptionHandlingMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                return next(context);
            }
            catch (NotFoundException e)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsJsonAsync(Dto(e));
            }
            catch (AlreadyExistsException e)
            {
                context.Response.StatusCode = 409;
                return context.Response.WriteAsJsonAsync(Dto(e));
            }
        }

        static object Dto(Exception e) => new {error = e.Message};
    }
}