using Klir.TechChallenge.Application.Products.Models;
using Klir.TechChallenge.Application.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Application.ShoppingCart.Models
{
    public record CartDTO
    {
        public Guid Id { get; set; }
        public ICollection<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal Total { get; set; }

        public static CartDTO ToCartDTO(Cart cart)
        {
            return new CartDTO
            {
                Id = cart.Id,
                Total = cart.Total,
                Items = cart.Items.Select(item => new CartItemDTO
                {
                    Id = item.Id,
                    CartId = cart.Id,
                    IsPromotionApplied = item.IsPromotionApplied,
                    TotalPrice = item.TotalPrice,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Product = new ProductDTO
                    {
                        Id = item.ProductId,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        PromotionId = item.Product.PromotionId,
                        IsActive = item.Product.IsActive,
                        Promotion = item.Product.Promotion is null ? null : new PromotionDTO
                        {
                            Id = item.Product.Promotion.Id,
                            Name = item.Product.Promotion.Name,
                            Description = item.Product.Promotion.Description,
                            IsActive = item.Product.Promotion.IsActive,
                            RequiredQuantity = item.Product.Promotion.RequiredQuantity,
                            Type = item.Product.Promotion.Type,
                        }
                    }
                }).OrderBy(x => x.Product.Name).ToList()
            };
        }
    }
}
