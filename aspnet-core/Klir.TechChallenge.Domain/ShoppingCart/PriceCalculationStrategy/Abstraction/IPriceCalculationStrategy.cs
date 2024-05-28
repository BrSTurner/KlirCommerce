using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction
{
    public interface IPriceCalculatorStrategy
    {
        decimal CalculateTotalPrice(CartItem cartItem);
    }
}
