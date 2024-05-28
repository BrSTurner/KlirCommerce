using Klir.TechChallenge.Core.Events;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Core.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Core.Extensions
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            return services;    
        }
    }
}
