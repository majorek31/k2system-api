using WebAPI.Common;

namespace WebAPI.Entities;
public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}
public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public ICollection<OrderItem> OrderItems { get; set; }

    public decimal TotalAmount => OrderItems.Sum(i => i.TotalPrice);
}