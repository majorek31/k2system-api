using System.Security.Cryptography.X509Certificates;
using WebAPI.Common;

namespace WebAPI.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public int QuantityInStock { get; set; }
    public decimal Price { get; set; }
    public string Manufacturer { get; set; }
    public string Tag { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
}