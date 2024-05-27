using Klir.TechChallenge.Application.Models;
using Klir.TechChallenge.Application.Products.Models;

namespace Klir.TechChallenge.Application.Products.Queries
{
    public interface IProductQueries
    {
        Task<PagedResult<ProductDTO>> GetAllPagedAsync(int pageSize, int pageIndex);
    }
}
