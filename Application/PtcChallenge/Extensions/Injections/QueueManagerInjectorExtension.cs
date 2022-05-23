using Domain.AuxModels;
using Domain.Interfaces.Services.Auxs;
using Services.Services.Auxs;

namespace PtcChallenge.Extensions.Injections
{
    public static class QueueManagerInjectorExtension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
            collection.AddSingleton<IQueueManager, QueueManager>();

            return collection;
        }
    }
}
