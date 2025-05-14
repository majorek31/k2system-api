using WebAPI.Entities;

namespace WebAPI.Repositories.ScopeRepository;

public interface IScopeRepository : IRepository<Scope>
{
    public Task<IEnumerable<Scope>> GetScopesByUserAsync(User user);
}