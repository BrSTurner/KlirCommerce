using Klir.TechChallenge.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Core.Extensions
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;    
        }
    }
}
