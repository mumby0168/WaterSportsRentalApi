using SplashRentals.Domain.Abstractions;

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
            Id = win;
            AssetType = assetType;
            Occupancy = occupancy;
            Win = win;
        }

        internal Asset() { }
    }
}