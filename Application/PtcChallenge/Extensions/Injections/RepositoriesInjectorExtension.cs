using Data.Entities;
using Data.Repositories;
using Domain.Interfaces.Repositories;

namespace PtcChallenge.Extensions.Injections
{
    public static class RepositoriesInjectorExtension
    {
        /// <summary>
        /// Adds repositories configuration to injection container.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Container injected.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IBrandRepository<Brand>, BrandRepository>();
            collection.AddScoped<IOwnerRepository<Owner>, OwnerRepository>();
            collection.AddScoped<IVehicleRepository<Vehicle>, VehicleRepository>();

            return collection;
        }
    }
}
