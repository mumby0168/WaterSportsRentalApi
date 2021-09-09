using System;
using SplashRentals.Domain.Abstractions;

namespace SplashRentals.Domain.Entities
{
    public class AssetType : AggregateRoot
    {
        public string Name { get; }

        public AssetType(string name, string? id)
        {
            Name = name;
            Id = id ?? Guid.NewGuid().ToString();
        }
    }
}