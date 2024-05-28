using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;

namespace Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy
{
    public class RegularPriceCalculatorStrategy : IPriceCalculatorStrategy
    {
        public decimal CalculateTotalPrice(CartItem cartItem)
        {
            var totalPrice = cartItem.Quantity * cartItem.UnitPrice;
            cartItem.SetTotalPrice(totalPrice);
            return totalPrice;
        }
    }
}

