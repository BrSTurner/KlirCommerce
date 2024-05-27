using Klir.TechChallenge.Application.Products.Models;

namespace Klir.TechChallenge.Application.ShoppingCart.Models
{
    public record CartItemDTO
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public CartDTO Cart { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsPromotionApplied { get; set; }
    }
}
