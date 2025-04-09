using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using MethodTeams.Models;
using Microsoft.EntityFrameworkCore;


namespace MethodologyMain.Persistence.Repository
{
    public class TeamRepository : GenericRepository<TeamEntity>, ITeamRepository
    {
        public TeamRepository(MyDbContext context) : base(context)
        {
        }
        public async Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId)
        {
            // User cannot be null
            var user = await _context.Users.
                AsNoTracking().
                FirstAsync(e => e.Id == userId);
            return user.Teams.Find(m => m.Team.HackathonId == hackathonId) 
                != null;
        }
        public async Task<bool> CheckUserInTeamAsync(Guid userId, Guid teamId)
        {
            // Team cannot be null checked before
            var team = await _context.Teams.
                AsNoTracking().
                FirstAsync(e => e.Id == teamId);
            return team.Members.Find(m => m.UserId == userId) 
                != null;
        }
        public async Task<bool> CheckTeamExistAsync(Guid teamId)
        {
            // Team cannot be null checked before
            var team = await _context.Teams.
                AsNoTracking().
                FirstAsync(e => e.Id == teamId);
            return team != null;
        }
        public async Task AddMemberAsync(Guid userId, Guid teamId)
        {
            // Team cannot be null checked before
            var team = await _context.Teams.FindAsync(teamId);
            team.Members.Add(new UserTeamEntity
            {
                TeamId = teamId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
        public async Task RemoveTeamAsync(Guid teamId)
        {
            // Удаление связанных участников (maybe not required)
            var team = await _context.Teams.FindAsync(teamId);
            if (team.Members is not null) { var _ = team.Members.RemoveAll; }
            // Удаление команды
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMemberAsync(Guid userId, Guid teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var member = team.Members.Find(e => e.UserId == userId);
            team.Members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task TransferCaptainAsync(Guid newCaptainId, Guid teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var member = team.Members.Find(e => e.UserId == newCaptainId);
            team.CaptainId = newCaptainId;
            await _context.SaveChangesAsync();
        }
        public async Task<Guid> GetCaptainIdAsync(Guid teamId)
        {
            var team = await _context.Teams.
                AsNoTracking().
                FirstAsync(e => e.Id == teamId);
            return team.CaptainId;
        }
        public async Task<Guid> GetHackathonIdAsync(Guid teamId)
        {
            var team = await _context.Teams.
                AsNoTracking().
                FirstAsync(e => e.Id == teamId);
            return team.HackathonId;
        }
        public async Task<List<Guid>> GetTeamMembersAsync(Guid teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            return team.Members.Select(m => m.UserId).ToList();
        }
    }
}
