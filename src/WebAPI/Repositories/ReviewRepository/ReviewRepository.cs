using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.ReviewRepository;

public class ReviewRepository(AppDbContext context) : Repository<Review>(context), IReviewRepository
{
    
}