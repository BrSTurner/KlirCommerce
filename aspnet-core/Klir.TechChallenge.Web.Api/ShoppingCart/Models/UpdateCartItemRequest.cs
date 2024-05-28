namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public record UpdateCartItemRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
