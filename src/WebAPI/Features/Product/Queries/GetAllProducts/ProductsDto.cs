namespace WebAPI.Features.Product.Queries.GetAllProducts;

public record ProductDto(string Name, string Description, int QuantityInStock, DateTime CreatedAt, decimal Price);

public record ProductsDto(IEnumerable<ProductDto> Products);
