namespace WebAPI.Dtos;

public record ProductDto(int Id, string Name, string Description, int QuantityInStock, DateTime CreatedAt, decimal Price, ICollection<ProductImageDto> ProductImages);
