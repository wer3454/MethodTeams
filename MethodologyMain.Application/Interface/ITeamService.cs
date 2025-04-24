using MethodologyMain.Logic.Entities;
using MethodTeams.DTO;

namespace MethodologyMain.Application.Interface
{
    public interface ITeamService
    {
        Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task<TeamEntity> CreateTeamAsync(string name, string description, Guid captainId, Guid eventId, CancellationToken token);
        Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        Task<TeamEntity> GetTeamByIdAsync(Guid teamId, CancellationToken token);
        Task<List<Guid>> GetTeamMembersAsync(Guid teamId, CancellationToken token);
        Task<List<TeamEntity>> GetTeamAllAsync(CancellationToken token);
        Task UpdateTeamAsync(Guid teamId, TeamInfoDto team, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        //Task<List<TeamEntity>> GetTeamsByEventIdAsync(Guid eventId);
        //Task<TeamEntity> GetUserTeamForEventAsync(Guid userId, Guid eventId);
        //Task<bool> IsUserTeamCaptainAsync(Guid teamId, Guid userId);
        Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId, CancellationToken token);
    }
}