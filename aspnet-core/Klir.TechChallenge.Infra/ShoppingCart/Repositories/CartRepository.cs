using Klir.TechChallenge.Domain.ShoppingCart.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;
using Klir.TechChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Klir.TechChallenge.Infra.ShoppingCart.Repositories
{
    public sealed class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(KlirCommerceContext context) : base(context)
        {
        }

        public void Update(Cart cart) => _entity.Update(cart);

        public async Task AddAsync(Cart cart) => await _entity.AddAsync(cart);

        public async Task<Cart?> GetAsync(Guid id) => await _entity
                                                                .Include(i => i.Items)
                                                                .ThenInclude(p => p.Product)
                                                                .ThenInclude(p => p.Promotion)
                                                                .FirstOrDefaultAsync(c => c.Id.Equals(id));
    }
}
