using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetAsync(Guid id);
        Task AddAsync(Cart cart);
        void Update(Cart cart);
    }
}
