using Klir.TechChallenge.Domain.Products.Models;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Klir.TechChallenge.Infra.Products.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(KlirCommerceContext context) : base(context)
        {
        }

        public async Task<Product?> GetAsync(Guid id) => await _entity
                                                                  .Include(p => p.Promotion)
                                                                  .FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<Product>> GetMultipleByIdNoTrackingAsync(IList<Guid> ids)
        {
            return await _entity
                            .AsNoTracking()
                            .Include(x => x.Promotion)
                            .Where(x => ids.Contains(x.Id))
                            .ToListAsync();
        }
    }
}
