using MethodologyMain.Logic.Entities;
using MethodTeams.Models;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface ITeamRepository : IGenericRepository<TeamEntity>
    {
        Task AddMemberAsync(Guid userId, Guid teamId);
        Task<bool> CheckUserInTeamAsync(Guid userId, Guid teamId);
        Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId);
        Task<bool> CheckTeamExistAsync(Guid teamId);
        Task RemoveTeamAsync(Guid teamId);
        Task RemoveMemberAsync(Guid userId, Guid teamId);
        Task TransferCaptainAsync(Guid newCaptainId, Guid teamId);
        Task<Guid> GetCaptainIdAsync(Guid teamId);
        Task<Guid> GetHackathonIdAsync(Guid teamId);
        Task<List<Guid>> GetTeamMembersAsync(Guid teamId);
    }

}
