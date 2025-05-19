using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Product.Commands.CreateProduct;

public record CreateProductCommand(CreateProductDto Dto) : IRequest<Entities.Product>;