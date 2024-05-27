namespace Klir.TechChallenge.Application.ShoppingCart.Models
{
    public record CartDTO
    {
        public Guid Id { get; set; }
        public ICollection<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal Total { get; set; }
    }
}
