﻿namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public record CreateCartRequest
    {
        public ICollection<CreateCartItemRequest> Items { get; set; } = new List<CreateCartItemRequest>();
    }
}
