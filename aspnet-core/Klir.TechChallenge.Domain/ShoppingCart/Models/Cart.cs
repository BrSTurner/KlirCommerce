using Klir.TechChallenge.Core.DomainObjects;

namespace Klir.TechChallenge.Domain.ShoppingCart.Models
{
    public class Cart : Entity, IAggregateRoot
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public virtual ICollection<CartItem> Items { get; set; }
        public decimal Total { get; set; }

        public void AddToTotal(decimal value) => Total += value;
        public void AddItem(CartItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
            Total = 0;
        }
    }
}
