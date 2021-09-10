using FluentAssertions;
using SplashRentals.Domain.Entities;
using SplashRentals.Domain.Exceptions;
using Xunit;

namespace SplashRentals.Domain.Tests.Entities
{
    public class AssetTests
    {
        [Fact]
        public void Asset_NoWin_ThrowsDomainException() =>
            Assert.Throws<DomainException>(() => new Asset("", new AssetType("name"), 1));
        
        [Fact]
        public void Asset_OccupancyLessThanOne_ThrowsDomainException() =>
            Assert.Throws<DomainException>(() => new Asset("123", new AssetType("name"), 0));

        [Fact]
        public void Asset_ValidData_CreatesAsset()
        {
            var asset = new Asset("123", new AssetType("name", "1"), 1);

            asset.Id.Should().Be("123");
            asset.Win.Should().Be("123");
            asset.Occupancy.Should().Be(1);

            asset.AssetType.Name.Should().Be("name");
            asset.AssetType.Id.Should().Be("1");
        }
        
        [Fact]
        public void Asset_AssetTypeWithNoName_ThrowsDomainException() =>
            Assert.Throws<DomainException>(() => new Asset("123", new AssetType(""), 1));

    }
}