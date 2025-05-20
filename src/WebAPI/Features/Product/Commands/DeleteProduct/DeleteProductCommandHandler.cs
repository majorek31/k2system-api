using Mapster;
using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Product.Commands.DeleteProduct;

class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            throw new NotFoundException();
        await productRepository.DeleteAsync(product);
        return Unit.Value;
    }
}