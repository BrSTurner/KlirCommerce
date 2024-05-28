using Klir.TechChallenge.Domain.Promotions.Models;

namespace Klir.TechChallenge.Tests
{
    public class MockPromotionTable
    {
        private static MockPromotionTable _instance { get; set; }

        public IList<Promotion> Promotions { get; set; }
        
        private MockPromotionTable() 
        {
            Promotions = new List<Promotion>
            {
                new Promotion
                {
                    Id = Guid.Parse("0d7f27a1-d707-41e4-a9fd-8d7e2d312120"),
                    IsActive = true,
                    Description = "Nice Promotion",
                    Name = "Buy One Get One",
                    RequiredQuantity = 2,
                    Type = PromotionType.BuyOneGetOne
                },
                new Promotion
                {
                    Id = Guid.Parse("bc7b57c6-6472-4e70-8273-1a2263742640"),
                    IsActive = true,
                    Description = "Great Promotion",
                    Name = "3 for 10 Euro",
                    RequiredQuantity = 3,
                    Type = PromotionType.ThreeForTen
                }
            };
        }

        public static MockPromotionTable GetInstance()
        {
            if (_instance is null)
                _instance = new MockPromotionTable();

            return _instance;
        }
    }
}
