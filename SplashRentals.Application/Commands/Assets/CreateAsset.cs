using System.Collections.Generic;
using MediatR;
using SplashRentals.Application.Dtos;

namespace SplashRentals.Application.Commands.Assets
{
    public record CreateAsset(string Win, int Occupancy, AssetTypeDto AssetType) : IRequest;
}