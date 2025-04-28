using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;

namespace MethodologyMain.Application.Interface
{
    public interface ITeamService
    {
        Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task<GetTeamDto> CreateTeamAsync(CreateTeamDto dto, Guid captainId, CancellationToken token);
        Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        Task<GetTeamDto> GetTeamByIdAsync(Guid teamId, CancellationToken token);
        Task<List<string>> GetTeamMembersAsync(Guid teamId, CancellationToken token);
        Task<List<GetTeamDto>> GetTeamAllAsync(CancellationToken token);
        Task<List<GetTeamDto>> GetTeamForHackathonAsync(Guid hackathonId, CancellationToken token);
        Task UpdateTeamAsync(UpdateTeamDto team, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        //Task<List<TeamEntity>> GetTeamsByEventIdAsync(Guid eventId);
        //Task<TeamEntity> GetUserTeamForEventAsync(Guid userId, Guid eventId);
        //Task<bool> IsUserTeamCaptainAsync(Guid teamId, Guid userId);
        Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId, CancellationToken token);
    }
}