using Klir.TechChallenge.Domain.Products.Models;
using Klir.TechChallenge.Domain.Promotions.Models;

namespace Klir.TechChallenge.Tests
{
    public class MockProductTable
    {
        private static MockProductTable _instance { get; set; }

        public IList<Product> Products { get; set; }

        private MockProductTable()
        {
            Products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("f1cdda72-5a81-48f1-a54d-cc0a6330bf84"),
                    Name = "Product A",
                    Price = 20.00m,
                    PromotionId = Guid.Parse("0d7f27a1-d707-41e4-a9fd-8d7e2d312120"),
                    IsActive = true,
                    Promotion = MockPromotionTable.GetInstance().Promotions.FirstOrDefault(x => x.Type == PromotionType.BuyOneGetOne)
                },
                new Product
                {
                    Id = Guid.Parse("e129cee7-e978-434e-9a7a-0364bbdfd1e7"),
                    Name = "Product B",
                    Price = 4.00m,
                    PromotionId = Guid.Parse("bc7b57c6-6472-4e70-8273-1a2263742640"),
                    IsActive = true,
                    Promotion = MockPromotionTable.GetInstance().Promotions.FirstOrDefault(x => x.Type == PromotionType.ThreeForTen)
                },
                new Product
                {
                    Id = Guid.Parse("d81f1995-0362-40b1-9657-ceb81f709a25"),
                    Name = "Product C",
                    Price = 2.00m,
                    IsActive = true,
                },
                new Product
                {
                    Id = Guid.Parse("16a0248d-3ceb-4369-bdf0-0e93b7e93759"),
                    Name = "Product D",
                    Price = 4.00m,
                    PromotionId = Guid.Parse("bc7b57c6-6472-4e70-8273-1a2263742640"),
                    IsActive = true,
                    Promotion = MockPromotionTable.GetInstance().Promotions.FirstOrDefault(x => x.Type == PromotionType.ThreeForTen)
                }
            };
        }

        public static MockProductTable GetInstance()
        {
            if (_instance is null)
                _instance = new MockProductTable();

            return _instance;
        }

    }
}
