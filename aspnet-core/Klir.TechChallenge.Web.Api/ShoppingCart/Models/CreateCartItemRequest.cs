﻿namespace Klir.TechChallenge.Web.Api.ShoppingCart.Models
{
    public record CreateCartItemRequest
    {
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
