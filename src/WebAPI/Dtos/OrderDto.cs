namespace WebAPI.Dtos;

public record OrderDto(int Id,
    string PostalCode,
    string City,
    string StreetName,
    int BuildingNumber,
    int? FlatNumber,
    string OrderStatus,
    decimal TotalAmount,
    ICollection<ProductDto> OrderItems);