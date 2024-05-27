using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Klir.TechChallenge.Core.Extensions;
using Klir.TechChallenge.Infra.Extensions;

namespace Klir.TechChallenge.Infra.IoC
{
    public static class DependencyInjecton
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
