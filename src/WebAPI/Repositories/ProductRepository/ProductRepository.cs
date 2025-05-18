using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.ProductRepository;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    
}