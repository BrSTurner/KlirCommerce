using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Core.Mediator;
using Klir.TechChallenge.Web.Api.Abstraction;
using Klir.TechChallenge.Web.Api.Models;
using Klir.TechChallenge.Web.Api.ShoppingCart.Models;

namespace Klir.TechChallenge.Web.Api.Cart.Endpoints
{
    public class CartEndpoints : IEndpointBuilder
    {
        public void MapEndpoints(IEndpointRouteBuilder endpointBuilder)
        {
            var group = endpointBuilder.MapGroup("/api/cart");

            group.MapPost(string.Empty, async (CreateCartRequest request, IMediatorHandler mediator) =>
            {
                if (request is null)
                    return Results.BadRequest(CustomResponse.ErrorResponse(new List<string> { "Request incorrectly filled" }));

                var command = new CreateCartCommand
                {
                    Items = request.Items.Select(x => new CartItemDTO
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                    })
                };

                var result = await mediator.SendCommand<Guid, CreateCartCommand>(command);

                if (result.Success)
                    return Results.Created($"/api/cart/{result.Data}", CustomResponse.SuccessResponse(result.Data));

                return Results.BadRequest();
            });
        }
    }
}
