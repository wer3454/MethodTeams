using MethodTeams.Models;

namespace MethodTeams.Interface
{
    public interface ITeamService
    {
        Task AddUserToTeamAsync(int teamId, int userId, int requestingUserId);
        Task<Team> CreateTeamAsync(string name, string description, int captainId, int eventId);
        Task DeleteTeamAsync(int teamId, int requestingUserId, bool isAdmin = false);
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<List<int>> GetTeamMembersAsync(int teamId);
        Task<List<Team>> GetTeamsByEventIdAsync(int eventId);
        Task<Team> GetUserTeamForEventAsync(int userId, int eventId);
        Task<bool> IsUserTeamCaptainAsync(int teamId, int userId);
        Task RemoveUserFromTeamAsync(int teamId, int userId, int requestingUserId);
        Task TransferCaptainRightsAsync(int teamId, int newCaptainId, int currentCaptainId);
    }
}