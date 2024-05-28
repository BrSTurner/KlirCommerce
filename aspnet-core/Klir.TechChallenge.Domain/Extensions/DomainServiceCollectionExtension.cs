using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;
using Klir.TechChallenge.Domain.ShoppingCart.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Domain.Extensions
{
    public static class DomainServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddKeyedSingleton<IPriceCalculatorStrategy, BuyOneGetOnePriceCalculatorStrategy>(PromotionType.BuyOneGetOne);
            services.AddKeyedSingleton<IPriceCalculatorStrategy, ThreeForTenPriceCalculatorStrategy>(PromotionType.ThreeForTen);
            services.AddKeyedSingleton<IPriceCalculatorStrategy, RegularPriceCalculatorStrategy>(PromotionType.None);

            services.AddScoped<ICartService, CartService>();

            return services;
        }
    }
}
