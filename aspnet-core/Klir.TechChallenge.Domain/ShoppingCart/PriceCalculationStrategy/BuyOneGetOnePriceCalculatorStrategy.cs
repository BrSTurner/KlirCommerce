using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;

namespace Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy
{
    public class BuyOneGetOnePriceCalculatorStrategy : IPriceCalculatorStrategy
    {
        public decimal CalculateTotalPrice(CartItem cartItem)
        {
            var requiredQuantity = cartItem.Product.Promotion.RequiredQuantity;

            var quantity = cartItem.Quantity;
            var unitPrice = cartItem.UnitPrice;

            if (quantity / requiredQuantity >= 1)
            {
                cartItem.ApplyPromotion();
            }

            var totalPrice = quantity / requiredQuantity * unitPrice + quantity % requiredQuantity * unitPrice;

            cartItem.SetTotalPrice(totalPrice);

            return totalPrice;
        }
    }
}
