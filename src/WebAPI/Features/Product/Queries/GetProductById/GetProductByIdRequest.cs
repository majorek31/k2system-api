using MediatR;
using WebAPI.Dtos;
using WebAPI.Features.Product.Queries.GetAllProducts;

namespace WebAPI.Features.Product.Queries.GetProductById;

public record GetProductByIdRequest(int Id) : IRequest<ProductDto?>;