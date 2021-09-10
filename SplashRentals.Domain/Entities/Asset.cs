using SplashRentals.Domain.Abstractions;
using SplashRentals.Domain.Exceptions;

namespace SplashRentals.Domain.Entities
{
    public class Asset : AggregateRoot
    {
        /// <summary>
        /// Water craft identification number.
        /// </summary>
        public string Win { get; }
        
        /// <summary>
        /// The type of asset.
        /// </summary>
        public AssetType AssetType { get; }

        /// <summary>
        /// Amount of people that can occupy the asset.
        /// </summary>
        public int Occupancy { get; }

        public Asset(string win, AssetType assetType, int occupancy)
        {
            if (string.IsNullOrWhiteSpace(win)) throw new DomainException("An asset must provide a win.");
            if (occupancy < 1) throw new DomainException("An asset must be able to occupy at least one person.");

            Id = win;
            AssetType = assetType;
            Occupancy = occupancy;
            Win = win;
        }

        internal Asset() { }
    }
}