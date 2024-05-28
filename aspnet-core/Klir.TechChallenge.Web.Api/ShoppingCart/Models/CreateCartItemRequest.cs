namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public class CreateCartItemRequest
    {
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
