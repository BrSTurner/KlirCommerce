namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public class CreateCartRequest
    {
        public ICollection<CreateCartItemRequest> Items { get; set; } = new List<CreateCartItemRequest>();
    }
}
