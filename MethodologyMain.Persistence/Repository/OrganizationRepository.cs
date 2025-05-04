using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;

namespace MethodologyMain.Persistence.Repository
{
    public class OrganizationRepository : GenericRepository<OrganizationEntity>, IOrganizationRepository
    {
        public OrganizationRepository(MyDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<OrganizationEntity>> GetAllAsync(CancellationToken token)
        {
            return await _context.Organizations.Include(h => h.Hackathons).AsNoTracking().ToListAsync(token);
        }

        public async Task<OrganizationEntity?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Organizations.Include(h => h.Hackathons).FirstOrDefaultAsync(m => m.Id == id, token);
        }
    }
}
