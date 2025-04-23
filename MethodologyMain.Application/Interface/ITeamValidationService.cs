namespace MethodologyMain.Application.Interface
{
    public interface ITeamValidationService
    {
        Task CheckCaptainKick(Guid teamId, Guid userId, CancellationToken token);
        Task CheckTeamExistsAsync(Guid teamId, CancellationToken token);
        Task CheckUserInTeamAsync(Guid userId, Guid teamId, CancellationToken token);
        Task CheckUserIsCaptainAsync(Guid teamId, Guid userId, CancellationToken token);
        Task CheckUserIsCaptainOrAdminAsync(Guid teamId, Guid userId, bool isAdmin, CancellationToken token);
        Task CheckUserIsCaptainOrRequestedAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token);
        Task CheckUserNotInAnyTeamForHackathonAsync(Guid userId, Guid hackathonId, CancellationToken token);
        Task CheckUserNotInTeamAsync(Guid userId, Guid teamId, CancellationToken token);
    }
}