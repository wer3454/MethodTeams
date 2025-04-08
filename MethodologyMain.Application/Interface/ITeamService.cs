using MethodTeams.Models;

namespace MethodologyMain.Application.Interface
{
    public interface ITeamService
    {
        Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId);
        Task<Team> CreateTeamAsync(string name, string description, Guid captainId, Guid eventId);
        Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, bool isAdmin = false);
        Task<Team> GetTeamByIdAsync(Guid teamId);
        Task<List<Guid>> GetTeamMembersAsync(Guid teamId);
        Task<List<Team>> GetTeamsByEventIdAsync(Guid eventId);
        Task<Team> GetUserTeamForEventAsync(Guid userId, Guid eventId);
        Task<bool> IsUserTeamCaptainAsync(Guid teamId, Guid userId);
        Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId);
        Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId);
    }
}