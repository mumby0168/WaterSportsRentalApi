using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.CosmosRepository;
using Moq;
using Moq.AutoMock;
using SplashRentals.Application.Infrastructure;
using SplashRentals.Domain.Entities;
using SplashRentals.Infrastructure.Repositories;
using SplashRentals.Infrastructure.Tests.Helpers;
using Xunit;

namespace SplashRentals.Infrastructure.Tests.Repositories
{
    public class AssetRepositoryTests
    {
        private readonly AutoMocker _mocker = new();
        private Mock<IRepository<Asset>> _assetsCosmosRepository;
        private Mock<IRepository<AssetType>> _assetTypeCosmosRepository;

        public AssetRepositoryTests()
        {
            _assetsCosmosRepository = _mocker.GetMock<IRepository<Asset>>();
            _assetTypeCosmosRepository = _mocker.GetMock<IRepository<AssetType>>();
        }

        private IAssetRepository Sut => _mocker.CreateInstance<AssetRepository>();

        [Fact]
        public async Task AssetTypeExistsAsync_TypeThatExists_ReturnsTrue()
        {
            //Arrange
            string assetTypeId = "1";

            //Act
            bool result = await Sut.AssetTypeExistsAsync(assetTypeId);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AssetTypeExistsAsync_TypeThatDoesNotExist_ReturnsFalse()
        {
            //Arrange
            string assetTypeId = "1";

            _assetTypeCosmosRepository
                .Setup(o => o.GetAsync(assetTypeId, null, CancellationToken.None))
                .Throws(CosmosExceptionHelper.ThrowCosmosNotFoundException());

            //Act
            bool result = await Sut.AssetTypeExistsAsync(assetTypeId);

            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async Task ExistsAsync_AssetThatExists_ReturnsTrue()
        {
            //Arrange
            string win = "1";

            //Act
            bool result = await Sut.ExistsAsync(win);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsAsync_AssetThatDoesNotExist_ReturnsFalse()
        {
            //Arrange
            string win = "1";

            _assetsCosmosRepository
                .Setup(o => o.GetAsync(win, null, CancellationToken.None))
                .Throws(CosmosExceptionHelper.ThrowCosmosNotFoundException());

            //Act
            bool result = await Sut.ExistsAsync(win);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_Asset_UpdatesTypeAndCreatesAssert()
        {
            //Arrange
            var asset = new Asset("123", new AssetType("a"), 5);

            //Act
            await Sut.CreateAsync(asset);

            //Assert
            _assetTypeCosmosRepository.Verify(o => o.UpdateAsync(asset.AssetType, CancellationToken.None));
            _assetsCosmosRepository.Verify(o => o.CreateAsync(asset, CancellationToken.None));
        }
    }
}