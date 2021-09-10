using System;
using SplashRentals.Domain.Abstractions;
using SplashRentals.Domain.Exceptions;

namespace SplashRentals.Domain.Entities
{
    public class AssetType : AggregateRoot
    {
        public string Name { get; }

        public AssetType(string name, string? id = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("An asset type must provide a name.");
            
            Name = name;
            Id = id ?? Guid.NewGuid().ToString();
        }
    }
}