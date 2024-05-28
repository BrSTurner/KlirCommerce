using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;

namespace Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy
{
    public class ThreeForTenPriceCalculatorStrategy : IPriceCalculatorStrategy
    {
        public decimal CalculateTotalPrice(CartItem cartItem)
        {
            var requiredQuantity = cartItem.Product.Promotion.RequiredQuantity;

            var quantity = cartItem.Quantity;

            if (quantity / requiredQuantity >= 1)
            {
                cartItem.ApplyPromotion();
            }

            var totalPrice = cartItem.Quantity / 3 * 10 + cartItem.Quantity % 3 * cartItem.UnitPrice;

            cartItem.SetTotalPrice(totalPrice);

            return totalPrice;
        }
    }
}
