using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Product.Queries.GetAllProducts;

public record GetAllProductsRequest() : IRequest<ProductsDto>;
