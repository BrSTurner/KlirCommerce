using Klir.TechChallenge.Core.DomainObjects;
using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.ShoppingCart.Models;

namespace Klir.TechChallenge.Domain.Products.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public Product()
        {
            CartItems = new List<CartItem>();
            IsActive = true;
        }

        public required string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public bool IsInactive
        {
            get
            {
                return !IsActive;
            }
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void SetPromotion(Promotion promotion)
        {
            PromotionId = promotion.Id;
            Promotion = promotion;
        }

        public void RemovePromotion()
        {
            PromotionId = null;
            Promotion = null;
        }

        public bool IsPromotionValid() => Promotion is null || Promotion.IsActive;
    }
}
