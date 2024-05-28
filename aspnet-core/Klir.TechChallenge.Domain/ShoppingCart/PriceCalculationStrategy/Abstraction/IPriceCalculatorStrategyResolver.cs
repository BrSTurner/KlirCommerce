using Klir.TechChallenge.Domain.Promotions.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction
{
    public interface IPriceCalculatorStrategyResolver
    {
        IPriceCalculatorStrategy GetPriceCalculator(PromotionType type = PromotionType.None);
    }
}
