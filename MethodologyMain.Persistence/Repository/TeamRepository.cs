using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;

namespace MethodologyMain.Persistence.Repository
{
    public class TeamRepository : GenericRepository<TeamEntity>, ITeamRepository
    {
        public TeamRepository(MyDbContext context) : base(context)
        {
        }
        public async Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId, CancellationToken token)
        {
            CheckCancellation(token);
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == userId, token);
            if (user == null) return true;
            return user.Teams.Find(m => m.Team.HackathonId == hackathonId) != null;
        }
        public async Task<bool> CheckUserInTeamAsync(Guid userId, Guid teamId, CancellationToken token)
        {
            var team = await GetTeamNoTrackingAsync(teamId, token);
            return team.Members.Find(m => m.UserId == userId) != null;
        }
        public async Task<bool> CheckTeamExistAsync(Guid teamId, CancellationToken token)
        {
            var team = await GetTeamNoTrackingAsync(teamId, token);
            return team != null;
        }
        public async Task AddMemberAsync(Guid userId, Guid teamId, CancellationToken token)
        {
            var team = await GetTeamAsync(teamId, token);
            team.Members.Add(new UserTeamEntity
            {
                TeamId = teamId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            });
            await SaveChangesAsync(token);
        }
        public async Task RemoveTeamAsync(Guid teamId, CancellationToken token)
        {
            var team = await GetTeamAsync(teamId, token);
            if (team.Members is not null) { var _ = team.Members.RemoveAll; }
            _context.Teams.Remove(team);
            
            await SaveChangesAsync(token);
        }
        public async Task RemoveMemberAsync(Guid userId, Guid teamId, CancellationToken token)
        {
            var team = await GetTeamAsync(teamId, token);
            var member = team.Members.Find(e => e.UserId == userId);
            team.Members.Remove(member);
            await SaveChangesAsync(token);
        }
        public async Task TransferCaptainAsync(Guid newCaptainId, Guid teamId, CancellationToken token)
        {
            var team = await GetTeamAsync(teamId, token);
            var member = team.Members.Find(e => e.UserId == newCaptainId);
            team.CaptainId = newCaptainId;
            await SaveChangesAsync(token);
        }
        public async Task<Guid?> GetCaptainIdAsync(Guid teamId, CancellationToken token)
        {
            var team = await GetTeamNoTrackingAsync(teamId, token);
            return team.CaptainId;
        }
        public async Task<Guid?> GetHackathonIdAsync(Guid teamId, CancellationToken token)
        {
            var team = await GetTeamNoTrackingAsync(teamId, token);
            return team.HackathonId;
        }
        public async Task<List<Guid>?> GetTeamMembersAsync(Guid teamId, CancellationToken token)
        {
            var team = await GetTeamAsync(teamId, token);
            return team.Members.Select(m => m.UserId).ToList();
        }
        public async Task UpdateTeamAsync(TeamEntity team, CancellationToken token)
        {
            CheckCancellation(token);
            _context.Update(team);
            await _context.SaveChangesAsync(token);
        }
        private static void CheckCancellation(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
        }
        private async Task<TeamEntity> GetTeamNoTrackingAsync(Guid teamId, CancellationToken token)
        {
            CheckCancellation(token);
            return await _context.Teams.Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == teamId, token);
        }
        private async Task<TeamEntity> GetTeamAsync(Guid teamId, CancellationToken token)
        {
            CheckCancellation(token);
            return await _context.Teams.Include(m => m.Members).FirstOrDefaultAsync(m => m.Id == teamId);
        }
        private async Task SaveChangesAsync(CancellationToken token)
        {
            CheckCancellation(token);
            await _context.SaveChangesAsync(token);
        }
    }
}