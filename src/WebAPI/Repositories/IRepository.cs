using WebAPI.Common;

namespace WebAPI.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> CountAsync();
}