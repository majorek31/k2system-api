using MediatR;

namespace WebAPI.Features.Product.Queries.GetAllProducts;

public record GetAllProductsRequest() : IRequest<ProductsDto>;
