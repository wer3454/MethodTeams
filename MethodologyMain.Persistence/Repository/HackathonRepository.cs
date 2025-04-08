using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Repository
{
    public class HackathonRepository : IHackathonRepository
    {
        private readonly MyDbContext context;
        public HackathonRepository(MyDbContext context)
        {
            this.context = context;
        }
        public async Task<List<HackathonEntity>> GetAllCurrentHackathonsAsync(int page = 1, int pageSize = 10)
        {
            return await context.Hackathons
                .AsNoTracking()
                .Where(h => h.EndDate > DateTime.UtcNow)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)                                
                .ToListAsync();
        }

        public async Task<HackathonEntity?> GetByIdAsync(Guid id)
        {
            return await context.Hackathons.FindAsync(id);
        }

        public async Task<List<HackathonEntity>> GetAllHackathonsAsync(int page = 1, int pageSize = 10)
        {
            return await context.Hackathons.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<HackathonEntity>> GetHackathonsByFlexibleSearchAsync(int page,int pageSize, Expression<Func<HackathonEntity, bool>> filter = null)
        {
            var query = context.Hackathons.AsQueryable();

            if(filter is not null) 
                query = query
                    .Where(filter);

            return await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
