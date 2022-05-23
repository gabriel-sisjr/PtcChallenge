using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Auxs;
using Services.Services;
using Services.Services.Auxs;

namespace PtcChallenge.Extensions.Injections
{
    public static class ServicesInjectorExtension
    {
        /// <summary>
        /// Adds services configuration to injection container.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Container injected.</returns>
        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            // Auxs
            collection.AddScoped<IAddressService, AddressService>();
            collection.AddScoped<IQueueManager, QueueManager>();

            // Main
            collection.AddScoped<IOwnerService, OwnerService>();
            collection.AddScoped<IBrandService, BrandService>();
            collection.AddScoped<IVehicleService, VehicleService>();
            return collection;
        }
    }
}
