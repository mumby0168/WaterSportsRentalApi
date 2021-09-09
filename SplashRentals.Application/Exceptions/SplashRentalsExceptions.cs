namespace SplashRentals.Application.Exceptions
{
    public static class SplashRentalsExceptions
    {
        public static void AssetTypeNotFound(string id) => 
            throw new NotFoundException($"An asset type with the id {id} could not be found.");
        
        public static void AssetNotFound(string id) => 
            throw new NotFoundException($"An asset with the id {id} could not be found.");

        public static void AssetAlreadyExists(string win) =>
            throw new AlreadyExistsException($"An asset with the win {win} already exists.");
    }
}