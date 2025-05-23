namespace WebAPI.Dtos;

public record CreateOrderDto(
    string PostalCode,
    string City,
    string StreetName,
    int BuildingNumber,
    int? FlatNumber,
    ICollection<CreateOrderProductDto> OrderItems);