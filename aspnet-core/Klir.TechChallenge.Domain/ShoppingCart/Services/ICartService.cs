using Klir.TechChallenge.Domain.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.Services
{
    public interface ICartService
    {
        Task<Result<Cart>> CalculateShoppingCart(Guid id);

    }
}
