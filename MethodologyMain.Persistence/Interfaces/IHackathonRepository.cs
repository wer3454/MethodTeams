using MethodologyMain.Logic.Entities;
using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface IHackathonRepository
    {
        Task<HackathonEntity?> GetByIdAsync(Guid id);

        Task<List<HackathonEntity>> GetAllCurrentHackathonsAsync(int page = 1, int pageSize = 10);

        Task<List<HackathonEntity>> GetAllHackathonsAsync(int page = 1, int pageSize = 10);

        Task<List<HackathonEntity>> GetHackathonsByFlexibleSearchAsync(int page, int pageSize, Expression<Func<HackathonEntity, bool>> filter = null);
    }
}
