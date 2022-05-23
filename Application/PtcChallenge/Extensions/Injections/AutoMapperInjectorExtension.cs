using AutoMapper;
using Data.Entities;
using Domain.Models;

namespace PtcChallenge.Extensions.Injections
{
    public static class AutoMapperInjectorExtension
    {
        /// <summary>
        /// Adds automapper configuration to injection container.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Container injected.</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection collection)
        {
            collection.AddSingleton(CreateMapping());
            return collection;
        }

        /// <summary>
        /// Create Mapper Configuration from Entity to Model and reverse.
        /// </summary>
        /// <returns>Mapper Configuration object.</returns>
        private static IMapper CreateMapping()
        {
            var configuration = new MapperConfiguration(c =>
            {
                c.CreateMap<Owner, OwnerModel>().ReverseMap();
                c.CreateMap<Brand, BrandModel>().ReverseMap();
                c.CreateMap<Vehicle, VehicleModel>().ReverseMap();
            });

            return configuration.CreateMapper();
        }
    }
}
