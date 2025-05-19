using WebAPI.Common;

namespace WebAPI.Entities;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string ImagePath { get; set; }
}