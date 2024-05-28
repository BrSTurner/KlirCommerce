using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Infra.Data;

namespace Klir.TechChallenge.Infra.ShoppingCart.Repositories
{
    public sealed class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(KlirCommerceContext context) : base(context)
        {
        }

        public async Task AddAsync(Cart cart) => await _entity.AddAsync(cart);
    }
}
