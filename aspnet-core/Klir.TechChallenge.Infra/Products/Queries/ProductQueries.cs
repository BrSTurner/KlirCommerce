using Dapper;
using Klir.TechChallenge.Application.Models;
using Klir.TechChallenge.Application.Products.Models;
using Klir.TechChallenge.Application.Products.Queries;
using Klir.TechChallenge.Application.Promotions.Models;
using Klir.TechChallenge.Domain.Products.Repositories;
using Klir.TechChallenge.Domain.Promotions.Models;

namespace Klir.TechChallenge.Infra.Products.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;
        public ProductQueries(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductDTO>> GetAllPagedAsync(int pageSize, int pageIndex)
        {
            var pagedResult = new PagedResult<ProductDTO>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            var sql = @$"SELECT 
                            P.Id,
                            P.Name,
                            P.Price,
                            P.IsActive,
                            P.PromotionId,
                            PM.Name AS PromotionName,
                            PM.Description AS PromotionDescription,
                            PM.RequiredQuantity,
                            PM.Type,
                            PM.IsActive AS IsPromotionActive
                         FROM 
                            Products AS P  
                      LEFT JOIN 
                            Promotions AS PM ON PM.Id=P.PromotionId
                      WHERE
                           P.IsActive=1
                      ORDER BY
                           P.Name
                      ASC
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Products";

            var result = await _productRepository.GetConnection().QueryMultipleAsync(sql);

            pagedResult.List = result.Read().Select(MapToProductDTO);
            pagedResult.TotalResults = result.Read<int>().FirstOrDefault();

            return pagedResult;
        }

        private ProductDTO MapToProductDTO(dynamic result)
        {
            return new ProductDTO
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                IsActive = result.IsActive,
                PromotionId = result.PromotionId,
                Promotion = string.IsNullOrEmpty(result.PromotionName) ? null : new PromotionDTO
                {
                    Id = result.PromotionId,
                    Name = result.PromotionName,
                    Description = result.PromotionDescription,
                    RequiredQuantity = result.RequiredQuantity,
                    Type = (PromotionType)result.Type,
                    IsActive = result.IsPromotionActive
                },
            };
        }
    }
}
