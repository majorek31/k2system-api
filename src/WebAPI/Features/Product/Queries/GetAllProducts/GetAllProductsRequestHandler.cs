using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Product.Queries.GetAllProducts;

public class GetAllProductsRequestHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsRequest, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAsync();
        return products.Adapt<IEnumerable<ProductDto>>();
    }
}