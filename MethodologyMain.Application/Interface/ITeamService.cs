using MethodologyMain.Logic.Entities;

namespace MethodologyMain.Application.Interface
{
    public interface ITeamService
    {
        Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId);
        Task<TeamEntity> CreateTeamAsync(string name, string description, Guid captainId, Guid eventId);
        Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, bool isAdmin = false);
        Task<TeamEntity> GetTeamByIdAsync(Guid teamId);
        Task<List<Guid>> GetTeamMembersAsync(Guid teamId);
        //Task<List<TeamEntity>> GetTeamsByEventIdAsync(Guid eventId);
        //Task<TeamEntity> GetUserTeamForEventAsync(Guid userId, Guid eventId);
        //Task<bool> IsUserTeamCaptainAsync(Guid teamId, Guid userId);
        Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId);
        Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId);
    }
}