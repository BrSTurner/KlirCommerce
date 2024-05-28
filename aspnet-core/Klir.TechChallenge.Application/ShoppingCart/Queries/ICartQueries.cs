using Klir.TechChallenge.Application.ShoppingCart.Models;

namespace Klir.TechChallenge.Application.ShoppingCart.Queries
{
    public interface ICartQueries
    {
        Task<CartDTO> GetByIdAsync(Guid id);
    }
}
