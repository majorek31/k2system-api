using Mapster;
using MediatR;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Entities.Product>
{
    public async Task<Entities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = request.Dto.Adapt<Entities.Product>();
        product.ProductImages = request.Dto.ImageUrls.Select(iu => new Entities.ProductImage{ImagePath = iu}).ToList();
        await productRepository.CreateAsync(product);
        return product;
    }
}