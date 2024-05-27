using Klir.TechChallenge.Application.Products.Queries;
using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Domain.Promotions.Repositories;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Infra.Data;
using Klir.TechChallenge.Infra.Products.Queries;
using Klir.TechChallenge.Infra.Products.Repositories;
using Klir.TechChallenge.Infra.Promotions.Repositories;
using Klir.TechChallenge.Infra.ShoppingCart.Repositories;
using Klir.TechChallenge.Infra.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infra.Extensions
{
    public static class InfraServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductQueries, ProductQueries>();

            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<KlirCommerceContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("KlirDatabase"));
            });

            return services;
        }
    }
}
