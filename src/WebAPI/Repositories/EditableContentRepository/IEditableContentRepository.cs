using WebAPI.Entities;

namespace WebAPI.Repositories.EditableContentRepository;

public interface IEditableContentRepository : IRepository<EditableContent>
{
    Task<EditableContent?> GetEditableContentAsync(string page, string key, string lang);
    Task<IEnumerable<EditableContent>> GetEditableContentAsync(string page, string lang);
}