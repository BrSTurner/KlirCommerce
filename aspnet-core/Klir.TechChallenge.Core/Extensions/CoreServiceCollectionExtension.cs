using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Core.Extensions
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificator, Notificator>();

            return services;    
        }
    }
}
