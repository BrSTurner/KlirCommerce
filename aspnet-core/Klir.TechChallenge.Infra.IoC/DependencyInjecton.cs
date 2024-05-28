using Klir.TechChallenge.Application.ShoppingCart.Handlers;
using Klir.TechChallenge.Core.Extensions;
using Klir.TechChallenge.Core.Handlers;
using Klir.TechChallenge.Infra.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infra.IoC
{
    public static class DependencyInjecton
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                typeof(DomainNotificationHandler).Assembly, 
                typeof(CreateCartHandler).Assembly));

            services.AddCore();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
