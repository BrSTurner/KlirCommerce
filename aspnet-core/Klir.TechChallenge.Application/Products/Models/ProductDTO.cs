using Klir.TechChallenge.Application.Promotions.Models;

namespace Klir.TechChallenge.Application.Products.Models
{
    public record ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid? PromotionId { get; set; }
        public PromotionDTO? Promotion { get; set; }
    }
}
