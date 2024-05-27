using Klir.TechChallenge.Core.DomainObjects;
using Klir.TechChallenge.Domain.Products.Models;

namespace Klir.TechChallenge.Domain.Promotions.Models
{
    public class Promotion : Entity, IAggregateRoot
    {
        public Promotion()
        {
            Products = new List<Product>();
            Activate();
        }

        public required string Name { get; set; }
        public required string Description { get; set; }
        public required PromotionType Type { get; set; }
        public int RequiredQuantity { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public bool Activate() => IsActive = true;
        public bool Deactivate() => IsActive = false;

        public void Update(string name, string description, int requiredQuantity)
        {
            Name = name;
            Description = description;
            RequiredQuantity = requiredQuantity;
        }
    }
}
