using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.EditableContentRepository;

public class EditableContentRepository(AppDbContext context)
    : Repository<EditableContent>(context), IEditableContentRepository
{
    public async Task<EditableContent?> GetEditableContentAsync(string page, string key, string lang)
    {
        return await context
            .EditableContents
            .Include(x => x.LastEditor)
            .Where(x => x.Page == page)
            .Where(x => x.Key == key)
            .Where(x => x.Language == lang)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<EditableContent>> GetEditableContentAsync(string page, string lang)
    {
        return await context
            .EditableContents
            .Include(x => x.LastEditor)
            .Where(x => x.Page == page)
            .Where(x => x.Language == lang)
            .AsNoTracking()
            .ToListAsync();
    }
}