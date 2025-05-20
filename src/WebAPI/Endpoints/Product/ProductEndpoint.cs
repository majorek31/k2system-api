using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.Product.Commands.CreateProduct;
using WebAPI.Features.Product.Queries.GetAllProducts;
using WebAPI.Features.Product.Queries.GetProductById;

namespace WebAPI.Endpoints.Product;

public class ProductEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("product");

        Configure<IEnumerable<ProductDto>>(
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllProductsRequest());
                return Results.Ok(result);
            }),
            "GetAllProducts",
            "Product",
            "Returns all products in database"
        );

        Configure<ProductDto>(
            group.MapGet("/{productId:int}", async (IMediator mediator, int productId) =>
            {
                var result = await mediator.Send(new GetProductByIdRequest(productId));
                return result is null ? Results.NotFound() : Results.Ok(result);
            })
            .Produces(StatusCodes.Status404NotFound),
            "GetProductById",
            "Product",
            "Returns product in database"
        );

        Configure<CreateProductDto, Unit>(
            group.MapPost("/", async (IMediator mediator, CreateProductDto dto) =>
                {
                    var result = await mediator.Send(new CreateProductCommand(dto));
                    var newLocation = $"/product/{result.Id}";
                    return Results.Created(newLocation, new());
                })
            .RequireScope("write:product")
            .Produces(StatusCodes.Status201Created),
            "CreateProduct",
            "Product",
            "Creates new product based on provided data"
            );
    }
}