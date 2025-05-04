using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface IHackathonRepository : IGenericRepository<HackathonEntity>
    {
        Task<HackathonEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<HackathonEntity>> GetHackathonsWithSearchAsync(SearchFilters filters, CancellationToken token);
        Task<List<HackathonEntity>> GetAllCurrentHackathonsAsync(int page = 1, int pageSize = 10, CancellationToken token = default);
        Task<List<HackathonEntity>> GetAllHackathonsAsync(CancellationToken token = default);
        Task<List<HackathonEntity>> GetAllHackathonsPagedAsync(int page = 1, int pageSize = 10, CancellationToken token = default);
        Task<List<HackathonEntity>> GetHackathonsByFlexibleSearchAsync(int page, int pageSize, Expression<Func<HackathonEntity, bool>> filter = null, CancellationToken token = default);
    }
}
