using Klir.TechChallenge.Domain.ShoppingCart.Models;
using System.Linq;

namespace Klir.TechChallenge.Tests
{
    public class MockCartTable
    {
        private static MockCartTable _instance { get; set; }

        public IList<Cart> Carts { get; set; }
        public IList<CartItem> CartItems { get; set; }

        private MockCartTable()
        {
            CartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartId = Guid.Parse("03f7471f-d4a9-4ada-8a40-7c0cea29fa03"),
                    IsPromotionApplied = false,
                    ProductId = Guid.Parse("f1cdda72-5a81-48f1-a54d-cc0a6330bf84"),
                    Quantity = 2,
                    TotalPrice = 0,
                    UnitPrice = 20,
                    Product = MockProductTable.GetInstance().Products.FirstOrDefault(x => x.Id.Equals(Guid.Parse("f1cdda72-5a81-48f1-a54d-cc0a6330bf84")))
                },
                new CartItem
                {
                    CartId = Guid.Parse("03f7471f-d4a9-4ada-8a40-7c0cea29fa03"),
                    IsPromotionApplied = false,
                    ProductId = Guid.Parse("e129cee7-e978-434e-9a7a-0364bbdfd1e7"),
                    Quantity = 3,
                    TotalPrice = 0,
                    UnitPrice = 4,
                    Product = MockProductTable.GetInstance().Products.FirstOrDefault(x => x.Id.Equals(Guid.Parse("e129cee7-e978-434e-9a7a-0364bbdfd1e7")))
                },
                new CartItem
                {
                    CartId = Guid.Parse("03f7471f-d4a9-4ada-8a40-7c0cea29fa03"),
                    IsPromotionApplied = false,
                    ProductId = Guid.Parse("d81f1995-0362-40b1-9657-ceb81f709a25"),
                    Quantity = 5,
                    TotalPrice = 0,
                    UnitPrice = 2,
                    Product = MockProductTable.GetInstance().Products.FirstOrDefault(x => x.Id.Equals(Guid.Parse("d81f1995-0362-40b1-9657-ceb81f709a25")))
                },
                new CartItem
                {
                    CartId = Guid.Parse("03f7471f-d4a9-4ada-8a40-7c0cea29fa03"),
                    IsPromotionApplied = false,
                    ProductId = Guid.Parse("16a0248d-3ceb-4369-bdf0-0e93b7e93759"),
                    Quantity = 2,
                    TotalPrice = 0,
                    UnitPrice = 4,
                    Product = MockProductTable.GetInstance().Products.FirstOrDefault(x => x.Id.Equals(Guid.Parse("16a0248d-3ceb-4369-bdf0-0e93b7e93759")))
                }
            };

            Carts = new List<Cart>
            {
                new Cart
                {
                    Id = Guid.Parse("03f7471f-d4a9-4ada-8a40-7c0cea29fa03"),
                    Total = 0,
                    Items = CartItems.ToList(),
                },
                new Cart
                {
                    Id = Guid.Parse("7c257c4a-5b53-4971-99d4-6c3d41ddda47"),
                    Items = Enumerable.Empty<CartItem>().ToList(),
                    Total = 0
                },
                new Cart
                {
                    Id = Guid.Parse("5e62b18c-63f3-447d-915a-1ece820c19f7"),
                    Items = CartItems.Take(2).ToList(),
                    Total = 0
                }
            };
            
        }

        public Cart GetCart(Guid id) => Carts.FirstOrDefault(x => x.Id.Equals(id));

        public static MockCartTable GetInstance()
        {          
            if (_instance is null)
                _instance = new MockCartTable();

            return _instance;            
        }
    } 
}
