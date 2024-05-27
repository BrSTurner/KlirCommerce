using Klir.TechChallenge.Domain.Products.Models;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Infra.Data;

namespace Klir.TechChallenge.Infra.Products.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(KlirCommerceContext context) : base(context)
        {
        }


    }
}
