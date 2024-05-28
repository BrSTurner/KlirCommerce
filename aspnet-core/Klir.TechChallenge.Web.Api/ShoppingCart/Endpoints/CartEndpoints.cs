using Klir.TechChallenge.Application.ShoppingCart.Commands;
using Klir.TechChallenge.Application.ShoppingCart.Models;
using Klir.TechChallenge.Application.ShoppingCart.Queries;
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

            group.MapGet("/{id:Guid:required}", async (Guid id, ICartQueries cartQueries) =>
            {
                if (id.Equals(Guid.Empty))
                    return Results.BadRequest(CustomResponse.ErrorResponse(new List<string> { "Cart Id is requied" }));

                return Results.Ok(CustomResponse.SuccessResponse(await cartQueries.GetByIdAsync(id)));
            });

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

            group.MapPut(string.Empty, async (UpdateCartRequest request, IMediatorHandler mediator) =>
            {
                if (request is null)
                    return Results.BadRequest(CustomResponse.ErrorResponse(new List<string> { "Request incorrectly filled" }));

                var command = new UpdateCartCommand
                {
                    Id = request.Id,
                    Items = request.Items.Select(x => new CartItemDTO
                    {
                        CartId = x.CartId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                    })
                };

                var result = await mediator.SendCommand<CartDTO, UpdateCartCommand>(command);

                if (result.Success)
                    return Results.Ok(CustomResponse.SuccessResponse(result.Data));

                return Results.BadRequest();
            });
        }
    }
}
