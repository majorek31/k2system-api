using MediatR;

namespace WebAPI.Features.Product.Commands.DeleteProduct;

public record DeleteProductCommand(int ProductId) : IRequest<Unit>;