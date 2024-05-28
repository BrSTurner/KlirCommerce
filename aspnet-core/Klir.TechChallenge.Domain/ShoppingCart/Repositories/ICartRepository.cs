using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task AddAsync(Cart cart);
    }
}
