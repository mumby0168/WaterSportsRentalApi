using Microsoft.Azure.Cosmos;

namespace SplashRentals.Infrastructure.Tests.Helpers
{
    public static class CosmosExceptionHelper
    {
        public static CosmosException ThrowCosmosNotFoundException() => new CosmosException(string.Empty, System.Net.HttpStatusCode.NotFound, 0, string.Empty, 0);
    }
}