﻿using Klir.TechChallenge.Core.Data;
using Klir.TechChallenge.Domain.Products.Models;

namespace Klir.TechChallenge.Domain.Products.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetMultipleByIdNoTrackingAsync(IList<Guid> ids);
    }
}
