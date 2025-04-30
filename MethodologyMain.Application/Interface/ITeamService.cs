using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MethodologyMain.Application.Interface
{
    public interface ITeamService
    {
        Task<GetTeamDto> CreateTeamAsync(CreateTeamDto dto, Guid captainId, CancellationToken token);
        Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        Task<GetTeamDto> GetTeamByIdAsync(Guid teamId, CancellationToken token);
        Task<List<string>> GetTeamMembersAsync(Guid teamId, CancellationToken token);
        Task<List<GetTeamDto>> GetTeamAllAsync(CancellationToken token);
        Task<List<GetTeamDto>> GetTeamForHackathonAsync(Guid hackathonId, CancellationToken token);
        Task UpdateTeamAsync(UpdateTeamDto team, Guid requestingUserId, CancellationToken token, bool isAdmin = false);
        Task JoinUserToTeamAsync(Guid teamId, Guid userId, CancellationToken token);
        Task LeaveUserFromTeamAsync(Guid teamId, Guid userId, CancellationToken token);
        //Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        //Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId, CancellationToken token);
    }
}