using Klir.TechChallenge.Application.Products.Queries;
using Klir.TechChallenge.Web.Api.Abstraction;
using Klir.TechChallenge.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Klir.TechChallenge.Web.Api.Products.Endpoints
{
    public class ProductEndpoints : IEndpointBuilder
    {
        public void MapEndpoints(IEndpointRouteBuilder endpointBuilder)
        {
            var productGroup = endpointBuilder.MapGroup("/api/products");

            productGroup.MapGet("/paged", async ([FromServices] IProductQueries productQueries, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 1) =>
            {
                if (pageSize <= 0 || pageIndex <= 0)
                {
                    return Results.BadRequest(CustomResponse.ErrorResponse(new List<string> { "Page Size or Page Index is not correctly filled." }));
                }

                return Results.Ok(CustomResponse.SuccessResponse(await productQueries.GetAllPagedAsync(pageSize, pageIndex)));
            });
        }
    }
}
