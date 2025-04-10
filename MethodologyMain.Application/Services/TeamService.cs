using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Application.Interface;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Application.Exceptions;

namespace MethodTeams.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamRepo;
        public TeamService(ITeamRepository teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        // Создание новой команды
        public async Task<TeamEntity> CreateTeamAsync(string name, string description, Guid captainId, Guid HackathonId)
        {
            // Проверка, что у пользователя нет другой команды для этого 
            if (await teamRepo.CheckUserTeamInHackAsync(captainId, HackathonId))
            {
                throw new MemberAlreadyInTeamException();
            }
            // Создание команды
            var team = new TeamEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CaptainId = captainId,
                HackathonId = HackathonId,
                TeamCreatedAt = DateTime.UtcNow
            };
            // Добавление капитана как участника команды
            var member = new UserTeamEntity
            {
                TeamId = team.Id,
                UserId = captainId,
                JoinedAt = DateTime.UtcNow
            };
            team.Members.Add(member);
            await teamRepo.AddAsync(team);
            return team;
        }

        // Удаление команды (только капитаном или администратором)
        public async Task DeleteTeamAsync(Guid teamId, Guid requestingUserId, bool isAdmin = false)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            if (await teamRepo.GetCaptainIdAsync(teamId) != requestingUserId && !isAdmin)
            {
                throw new UnauthorizedAccessException("Только капитан команды или администратор может удалить команду");
            }

            await teamRepo.RemoveTeamAsync(teamId);
        }

        // Добавление пользователя в команду
        public async Task AddUserToTeamAsync(Guid teamId, Guid userId, Guid requestingUserId)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            Guid hackathonId = await teamRepo.GetHackathonIdAsync(teamId);

            if (await teamRepo.GetCaptainIdAsync(teamId) != requestingUserId)
            {
                throw new UnauthorizedAccessException("Только капитан команды может добавлять участников");
            }

            // Проверка, что пользователь не состоит в команде этого события
            if (await teamRepo.CheckUserTeamInHackAsync(userId, hackathonId))
            {
                throw new MemberAlreadyInTeamException();
            }

            // Проверка, что пользователь не является уже членом этой команды
            if (await teamRepo.CheckUserInTeamAsync(userId, teamId))
            {
                throw new MemberAlreadyInTeamException();
            }

            // Добавление пользователя в команду
            await teamRepo.AddMemberAsync(userId, teamId);
        }

        // Удаление пользователя из команды
        public async Task RemoveUserFromTeamAsync(Guid teamId, Guid userId, Guid requestingUserId)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            // Проверка прав: капитан может удалить любого, участник - только себя
            Guid captainId = await teamRepo.GetCaptainIdAsync(teamId);
            bool canRemove = (captainId == requestingUserId) || (userId == requestingUserId);

            if (!canRemove)
            {
                throw new UnauthorizedAccessException("Недостаточно прав для удаления участника");
            }

            // Нельзя удалить капитана
            if (userId == captainId)
            {
                throw new InvalidOperationException("Капитан не может быть удален из команды. Сначала передайте права капитана другому участнику.");
            }

            if (!await teamRepo.CheckUserInTeamAsync(userId, teamId))
            {
                throw new UserNotFoundException();
            }

            await teamRepo.RemoveMemberAsync(userId, teamId);
        }

        // Передача прав капитана
        public async Task TransferCaptainRightsAsync(Guid teamId, Guid newCaptainId, Guid currentCaptainId)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            if (await teamRepo.GetCaptainIdAsync(teamId) != currentCaptainId)
            {
                throw new UnauthorizedAccessException("Только текущий капитан может передать права капитана");
            }

            // Проверка, что новый капитан является участником команды
            if (!await teamRepo.CheckUserInTeamAsync(newCaptainId, teamId))
            {
                throw new InvalidOperationException("Новый капитан должен быть участником команды");
            }

            // Передача прав
            await teamRepo.TransferCaptainAsync(newCaptainId, teamId);
        }

        // Получение информации о команде по ID
        public async Task<TeamEntity> GetTeamByIdAsync(Guid teamId)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            return await teamRepo.GetByIdAsync(teamId);
        }

        // Получение списка участников команды
        public async Task<List<Guid>> GetTeamMembersAsync(Guid teamId)
        {
            if (!await teamRepo.CheckTeamExistAsync(teamId))
            {
                throw new TeamNotFoundException();
            }

            return await teamRepo.GetTeamMembersAsync(teamId);
        }

        //// Проверка, является ли пользователь капитаном команды
        //public async Task<bool> IsUserTeamCaptainAsync(Guid teamId, Guid userId)
        //{
        //    var team = await _context.Teams.FindAsync(teamId);
        //    return team != null && await teamRepo.GetCaptainIdAsync(teamId) == userId;
        //}

        //// Получение команды пользователя для конкретного события
        //public async Task<TeamEntity> GetUserTeamForEventAsync(Guid userId, Guid eventId)
        //{
        //    return await _context.TeamMembers
        //        .Where(tm => tm.UserId == userId)
        //        .Include(tm => tm.Team)
        //        .Where(tm => tm.Team.EventId == eventId)
        //        .Select(tm => tm.Team)
        //        .FirstOrDefaultAsync();
        //}
        //// Получение списка команд для конкретного события
        //public async Task<List<TeamEntity>> GetTeamsByEventIdAsync(Guid eventId)
        //{
        //    return await _context.Teams
        //        .Where(t => t.EventId == eventId)
        //        .ToListAsync();
        //}
    }
}
