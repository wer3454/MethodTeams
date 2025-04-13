using MethodologyMain.Application.Exceptions;
using MethodologyMain.Application.Interface;
using MethodologyMain.Persistence.Interfaces;

namespace MethodologyMain.Application.Services
{
    class TeamValidationService : ITeamValidationService
    {
        public readonly ITeamRepository teamRepo;
        public TeamValidationService(ITeamRepository teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        // Проверка существования команды
        public async Task CheckTeamExistsAsync(Guid teamId, CancellationToken token)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId, token))
            {
                throw new TeamNotFoundException();
            }
        }
        // Проверка, что пользователь не состоит в команде для данного хакатона
        public async Task CheckUserNotInAnyTeamForHackathonAsync(Guid userId, Guid hackathonId, CancellationToken token)
        {
            if (await teamRepo.CheckUserTeamInHackAsync(userId, hackathonId, token))
            {
                throw new MemberAlreadyInTeamException();
            }
        }

        // Проверка, что пользователь не состоит в конкретной команде
        public async Task CheckUserNotInTeamAsync(Guid userId, Guid teamId, CancellationToken token)
        {
            if (await teamRepo.CheckUserInTeamAsync(userId, teamId, token))
            {
                throw new MemberAlreadyInTeamException();
            }
        }

        // Проверка, что пользователь состоит в команде
        public async Task CheckUserInTeamAsync(Guid userId, Guid teamId, CancellationToken token)
        {
            if (!await teamRepo.CheckUserInTeamAsync(userId, teamId, token))
            {
                throw new UserNotInTeamException();
            }
        }

        // Проверка, что пользователь имеет права капитана
        public async Task CheckUserIsCaptainAsync(Guid teamId, Guid userId, CancellationToken token)
        {
            if (await teamRepo.GetCaptainIdAsync(teamId, token) != userId)
            {
                throw new UnauthorizedAccessException();
            }
        }
        public async Task CheckUserIsCaptainOrAdminAsync(Guid teamId, Guid userId, bool isAdmin, CancellationToken token)
        {
            if (await teamRepo.GetCaptainIdAsync(teamId, token) != userId && !isAdmin)
            {
                throw new UnauthorizedAccessException();
            }
        }
        public async Task CheckUserIsCaptainOrRequestedAsync(Guid teamId, Guid userId, Guid requestingUserId, CancellationToken token)
        {
            Guid captainId = (Guid)await teamRepo.GetCaptainIdAsync(teamId, token);
            bool canRemove = (captainId == requestingUserId) || (userId == requestingUserId);
            if (!canRemove)
            {
                throw new UnauthorizedAccessException();
            }
        }
        public async Task CheckCaptainKick(Guid teamId, Guid userId, CancellationToken token)
        {
            if (await teamRepo.GetCaptainIdAsync(teamId, token) == userId)
            {
                throw new InvalidOperationException("Капитан не может быть удален из команды. Сначала передайте права капитана другому участнику.");
            }
        }
    }
}
