using Klir.TechChallenge.Domain.Promotions.Models;
using Klir.TechChallenge.Domain.Promotions.Repositories;
using Klir.TechChallenge.Infra.Data;

namespace Klir.TechChallenge.Infra.Promotions.Repositories
{
    public sealed class PromotionRepository : Repository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(KlirCommerceContext context) : base(context)
        {
        }
    }
}
