using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Product.Queries.GetAllProducts;

public class GetAllProductsRequestHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsRequest, ProductsDto>
{
    public async Task<ProductsDto> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAsync();
        return new ProductsDto(products.Adapt<IEnumerable<ProductDto>>());
    }
}