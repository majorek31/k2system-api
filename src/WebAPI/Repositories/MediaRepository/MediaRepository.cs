using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.MediaRepository;

public class MediaRepository(AppDbContext context) : Repository<Media>(context), IMediaRepository
{
    
}