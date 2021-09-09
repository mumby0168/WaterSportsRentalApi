using System;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using SplashRentals.Domain.Abstractions;
using SplashRentals.Domain.Entities;

namespace SplashRentals.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message){}
    }
}