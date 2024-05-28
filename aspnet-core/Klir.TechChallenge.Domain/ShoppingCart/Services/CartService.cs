using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.Models;
using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.PriceCalculationStrategy.Abstraction;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Domain.ShoppingCart.Services
{
    public class CartService : ICartService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            IServiceProvider serviceProvider,
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Cart>> CalculateShoppingCart(Guid id)
        {
            var cart = await _cartRepository.GetAsync(id);

            if (!CanCartBeCalculated(cart))
            {
                return Result<Cart>.Fail(cart ?? new Cart(), "We could not find your Shopping Cart, try again later.");
            }

            foreach (var item in cart.Items)
            {
                var product = item.Product;

                if (product.IsInactive)
                {
                    return Result<Cart>.Fail(cart, $"Product {product.Name} in your cart is no longer available.");
                }
                else if (!product.IsPromotionValid())
                {
                    return Result<Cart>.Fail(cart, $"Promotion {product.Promotion.Name} for the product {product.Name} is no longer available");
                }

                var priceCalculatorStrategy = _serviceProvider.GetRequiredKeyedService<IPriceCalculatorStrategy>(product.Promotion?.Type ?? PromotionType.None);

                if (priceCalculatorStrategy is null)
                {
                    return Result<Cart>.Fail($"Could not find a calculation strategy for the product: {product.Name}");
                }

                var totalPrice = priceCalculatorStrategy.CalculateTotalPrice(item);

                cart.AddToTotal(totalPrice);
            }

            cart.Total = cart.Items.Sum(x => x.TotalPrice);

            _cartRepository.Update(cart);

            if (!await _unitOfWork.CommitAsync())
            {
                return Result<Cart>.Fail("Unable to save your Shopping Cart");
            }

            return Result<Cart>.Ok(cart);
        }

        private bool CanCartBeCalculated(Cart? cart) => cart is not null;
    }
}
