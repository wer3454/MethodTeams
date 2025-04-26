using MethodologyMain.Logic.Entities;
using System.Threading.Tasks;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface ITeamRepository : IGenericRepository<TeamEntity>
    {
        Task AddMemberAsync(Guid userId, Guid teamId, CancellationToken token);
        Task<bool> CheckTeamExistAsync(Guid teamId, CancellationToken token);
        Task<bool> CheckUserInTeamAsync(Guid userId, Guid teamId, CancellationToken token);
        Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId, CancellationToken token);
        Task<Guid?> GetCaptainIdAsync(Guid teamId, CancellationToken token);
        Task<Guid?> GetHackathonIdAsync(Guid teamId, CancellationToken token);
        Task<List<Guid>?> GetTeamMembersAsync(Guid teamId, CancellationToken token);
        Task UpdateTeamAsync(TeamEntity team, CancellationToken token);
        Task<List<TeamEntity>> GetTeamsAllAsync(CancellationToken token);
        Task<TeamEntity> GetTeamAsync(Guid teamId, CancellationToken token);
        Task RemoveMemberAsync(Guid userId, Guid teamId, CancellationToken token);
        Task RemoveTeamAsync(Guid teamId, CancellationToken token);
        Task TransferCaptainAsync(Guid newCaptainId, Guid teamId, CancellationToken token);
    }

}
