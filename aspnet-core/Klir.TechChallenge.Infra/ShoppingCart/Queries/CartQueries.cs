using Dapper;
using Klir.TechChallenge.Application.Products.Models;
using Klir.TechChallenge.Application.Promotions.Models;
using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Application.ShoppingCart.Queries;
using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Repositories;

namespace Klir.TechChallenge.Infra.ShoppingCart.Queries
{
    public class CartQueries : ICartQueries
    {
        private readonly ICartRepository _cartRepository;

        public CartQueries(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDTO> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return new CartDTO();

            var sql = $@"SELECT
                            C.Id AS CartId,
                            C.Total AS CartTotal,
                            CI.Id AS ItemId,
                            CI.UnitPrice AS ItemUnitPrice,
                            CI.Quantity AS ItemQuantity,
                            CI.TotalPrice AS ItemTotalPrice,
                            CI.IsPromotionApplied AS ItemPromotionApplied,
                            P.Id AS ProductId,
                            P.Name AS ProductName,
                            P.Price AS ProductPrice,
                            P.IsActive AS ProductIsActive,
                            PM.Id AS PromotionId,
                            PM.Name AS PromotionName,
                            PM.Description AS PromotionDescription,
                            PM.Type AS PromotionType,
                            PM.RequiredQuantity AS PromotionRequiredQuantity,
                            PM.IsActive AS PromotionIsActive
                        FROM
                            ShoppingCart AS C
                        LEFT JOIN  
                            CartItems AS CI ON CI.CartId=C.Id
                        LEFT JOIN 
                            Products AS P ON P.Id=CI.ProductId
                        LEFT JOIN
                            Promotions AS PM ON PM.Id=P.PromotionId
                        WHERE   
                            C.Id=@CartId
                        ORDER BY
                            P.Name
                        SELECT COUNT(*) FROM CartItems WHERE CartId=@CartId";

            var parameters = new
            {
                CartId = id
            };

            var results = await _cartRepository.GetConnection().QueryMultipleAsync(sql, parameters);

            var cart = new CartDTO();
            var cartQueryResult = results.Read().ToList();

            cart.Id = cartQueryResult[0].CartId;
            cart.Total = cartQueryResult[0].CartTotal;

            var itemQuantity = results.Read<int>().First();

            if (itemQuantity <= 0)
                return cart;

            cart.Items = cartQueryResult.Select(MapToCartItemDTO).ToList();

            return cart;
        }

        private CartItemDTO MapToCartItemDTO(dynamic item)
        {
            return new CartItemDTO
            {
                Id = item.ItemId,
                CartId = item.CartId,
                UnitPrice = item.ItemUnitPrice,
                Quantity = item.ItemQuantity,
                IsPromotionApplied = item.ItemPromotionApplied,
                ProductId = item.ProductId,
                TotalPrice = item.ItemTotalPrice,
                Product = new ProductDTO
                {
                    Id = item.ProductId,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    IsActive = item.ProductIsActive,
                    PromotionId = item.PromotionId,
                    Promotion = item.PromotionId is null ? null : new PromotionDTO
                    {
                        Id = item.PromotionId,
                        Name = item.PromotionName,
                        Description = item.PromotionDescription,
                        RequiredQuantity = item.PromotionRequiredQuantity,
                        Type = (PromotionType)item.PromotionType,
                        IsActive = item.PromotionIsActive,
                    }
                }
            };

        }
    }
}
