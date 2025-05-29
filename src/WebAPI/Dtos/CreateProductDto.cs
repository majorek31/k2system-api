namespace WebAPI.Dtos;

public record CreateProductDto(string Name,
    string Description,
    string Sku,
    int QuantityInStock,
    decimal Price,
    string Manufacturer,
    string Tag,
    IEnumerable<string> ImageUrls);