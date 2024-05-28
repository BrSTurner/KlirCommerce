using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infra.ShoppingCart.Resolver
{
    public sealed class PriceCalculatorStrategyResolver : IPriceCalculatorStrategyResolver
    {
        private readonly IServiceProvider _serviceProvider;
        
        public PriceCalculatorStrategyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPriceCalculatorStrategy GetPriceCalculator(PromotionType type = PromotionType.None)
        {
            return _serviceProvider.GetRequiredKeyedService<IPriceCalculatorStrategy>(type);
        }
    }
}
