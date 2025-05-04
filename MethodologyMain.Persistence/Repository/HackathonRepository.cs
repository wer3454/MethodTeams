using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace MethodologyMain.Persistence.Repository
{
    public class HackathonRepository : GenericRepository<HackathonEntity>, IHackathonRepository
    {
        public HackathonRepository(MyDbContext context): base(context)
        {
        }
        public async Task<List<HackathonEntity>> GetAllCurrentHackathonsAsync(
            int page = 1, 
            int pageSize = 10,
            CancellationToken token = default
            )
        {
            return await _context.Hackathons
                .AsNoTracking()
                .Where(h => h.EndDate > DateOnly.FromDateTime(DateTime.Now))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)                                
                .ToListAsync(token);
        }

        public async Task<HackathonEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _context.Hackathons.Include(m => m.Organization).FirstOrDefaultAsync(m => m.Id == id, token);
        }
        public async Task<List<HackathonEntity>> GetAllHackathonsAsync(CancellationToken token = default)
        {
            return await _context.Hackathons.Include(m=>m.Organization).AsNoTracking().ToListAsync(token);
        }
        public async Task<List<HackathonEntity>> GetAllHackathonsPagedAsync(
            int page = 1, 
            int pageSize = 10, 
            CancellationToken token = default
            )
        {
            return await _context.Hackathons.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public async Task<List<HackathonEntity>> GetHackathonsByFlexibleSearchAsync(
            int page,
            int pageSize, 
            Expression<Func<HackathonEntity, bool>> filter = null,
            CancellationToken token = default
            )
        {
            var query = _context.Hackathons.AsQueryable();

            if(filter is not null) 
                query = query
                    .Where(filter);

            return await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);
        }
    }
}
