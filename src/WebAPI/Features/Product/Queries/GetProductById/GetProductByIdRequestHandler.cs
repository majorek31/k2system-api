using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Features.Product.Queries.GetAllProducts;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Product.Queries.GetProductById;

public class GetProductByIdRequestHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdRequest, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        return product.Adapt<ProductDto>();
    }
}