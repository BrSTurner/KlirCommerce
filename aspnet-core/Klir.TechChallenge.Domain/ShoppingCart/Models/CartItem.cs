using Klir.TechChallenge.Core.DomainObjects;
using Klir.TechChallenge.Domain.Products.Models;

namespace Klir.TechChallenge.Domain.ShoppingCart.Models
{
    public class CartItem : Entity
    {
        public required Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public required Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public required int Quantity { get; set; }
        public bool IsPromotionApplied { get; set; }

        public void AddQuantity(int quantity) => Quantity += quantity;
        public void SetTotalPrice(decimal totalPrice) => TotalPrice = totalPrice;
        public void ApplyPromotion() => IsPromotionApplied = true;
        public void RemovePromotion() => IsPromotionApplied = false;
        public void UpdateQuantity(int quantity) => Quantity = quantity;
        public void UpdateUnitPrice(decimal unitPrice) => UnitPrice = unitPrice;
    }
}
