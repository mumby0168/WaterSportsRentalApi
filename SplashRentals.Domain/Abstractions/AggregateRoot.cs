using System;
using Microsoft.Azure.CosmosRepository;

namespace SplashRentals.Domain.Abstractions
{
    public class AggregateRoot : Item
    {
        public new string Id { get; protected set; } = Guid.NewGuid().ToString();
    }
}