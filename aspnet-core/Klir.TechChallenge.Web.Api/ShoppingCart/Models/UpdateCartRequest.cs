namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public record UpdateCartRequest
    {
        public Guid Id { get; set; }
        public ICollection<UpdateCartItemRequest> Items { get; set; } = new List<UpdateCartItemRequest>();
    }
}
