using Mapster;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos;
using WebAPI.Entities;

namespace WebAPI.Extensions;

public static class MapsterExtensions
{
    public static void RegisterMapping()
    {
        TypeAdapterConfig<OrderItem, ProductDto>.NewConfig()
            .Map(dest => dest, src => src.Product);

        TypeAdapterConfig<Order, OrderDto>.NewConfig()
            .Map(dest => dest.OrderItems, src => src.OrderItems.Adapt<ICollection<ProductDto>>());
    }
}