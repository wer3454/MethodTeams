using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using MethodTeams.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Persistence.Repository
{
    public class UserRepository: GenericRepository<UserMainEntity>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context)
        {
        }
        public async Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId, CancellationToken token)
        {
            CheckCancellation(token);
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == userId, token);
            return user.Teams.Find(m => m.Team.HackathonId == hackathonId) != null;
        }
        public async Task RemoveAsync(Guid userId, CancellationToken token)
        {
            CheckCancellation(token);
            var user = await _context.Users.FindAsync([userId], token);
            _context.Users.Remove(user);
            await SaveChangesAsync(token);
        }
        public async Task UpdateAsync(UserMainEntity user, CancellationToken token)
        {
            CheckCancellation(token);
            _context.Update(user);
            await SaveChangesAsync(token);
        }
        public async Task<List<UserMainEntity>> GetUsersAllAsync(CancellationToken token)
        {
            CheckCancellation(token);
            return await _context.Users
                .Include(m => m.Tags)
                    .ThenInclude(i => i.Tag)
                .ToListAsync(token);
        }
        public async Task<UserMainEntity> GetUserByIdAsync(Guid userId,CancellationToken token)
        {
            CheckCancellation(token);
            return await _context.Users
                .Include(m => m.Tags)
                    .ThenInclude(i => i.Tag)
                .FirstOrDefaultAsync(m => m.Id == userId);
        }
        private static void CheckCancellation(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
        }
        private async Task SaveChangesAsync(CancellationToken token)
        {
            CheckCancellation(token);
            await _context.SaveChangesAsync(token);
        }

    }
}
