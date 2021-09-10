using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Moq.AutoMock;
using SplashRentals.Application.Commands.Assets;
using SplashRentals.Application.Dtos;
using SplashRentals.Application.Exceptions;
using SplashRentals.Application.Handlers.Commands.Assets;
using SplashRentals.Application.Infrastructure;
using SplashRentals.Domain.Entities;
using Xunit;

namespace SplashRentals.Application.Tests.Commands.Assets
{
    public class CreateAssetHandlerTests
    {
        private readonly AutoMocker _mocker = new();
        private readonly Mock<IAssetRepository> _assetRepository;

        public CreateAssetHandlerTests() =>
            _assetRepository = _mocker.GetMock<IAssetRepository>();

        private IRequestHandler<CreateAsset> Sut => _mocker.CreateInstance<CreateAssetHandler>();

        [Fact]
        public Task Handle_AssetAlreadyExists_ThrowsAlreadyExistsException()
        {
            _assetRepository.Setup(o => o.ExistsAsync("123")).ReturnsAsync(true);

            return Assert.ThrowsAsync<AlreadyExistsException>(() =>
                Sut.Handle(new CreateAsset("123", 2, new AssetTypeDto("1", "boat")), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_AssetTypeDoesNotExists_ThrowsNotFoundException() =>
            await Assert.ThrowsAsync<NotFoundException>(() =>
                Sut.Handle(new CreateAsset("123", 2, new AssetTypeDto("1", "boat")), CancellationToken.None));

        [Fact]
        public async Task Handle_ValidAsset_CreatesAsset()
        {
            _assetRepository.Setup(o => o.AssetTypeExistsAsync("1")).ReturnsAsync(true);
            var createCommand = new CreateAsset("123", 2, new AssetTypeDto("1", "boat"));

            await Sut.Handle(createCommand, CancellationToken.None);

            _assetRepository.Verify(o => o.CreateAsync(It.Is<Asset>(asset => ValidateAsset(asset, createCommand))));
        }

        static bool ValidateAsset(Asset asset, CreateAsset createAsset)
        {
            asset.Id.Should().Be(createAsset.Win);
            asset.Win.Should().Be(createAsset.Win);
            asset.Occupancy.Should().Be(createAsset.Occupancy);
            asset.AssetType.Id.Should().Be(createAsset.AssetType.Id);
            asset.AssetType.Name.Should().Be(createAsset.AssetType.Name);
            return true;
        }
    }

}
