using Klir.TechChallenge.Domain.Promotions.Models;

namespace Klir.TechChallenge.Application.Promotions.Models
{
    public record PromotionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PromotionType Type { get; set; }
        public int RequiredQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
