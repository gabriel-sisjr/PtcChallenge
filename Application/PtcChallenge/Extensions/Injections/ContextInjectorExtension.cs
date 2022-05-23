using Data.Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace PtcChallenge.Extensions.Injections
{
    public static class ContextInjectorExtension
    {
        /// <summary>
        /// Adds context configuration to injection container.
        /// </summary>
        /// <param name="collection">Configuration object to get AppSettings information.</param>
        /// <returns>Container injected.</returns>
        public static IServiceCollection AddContext(this IServiceCollection collection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ContainerDatabase");
            collection.AddDbContext<ContextDB>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            return collection;
        }
    }
}
