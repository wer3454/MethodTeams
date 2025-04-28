using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Application.Interface;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Application.Exceptions;
using MethodTeams.Models;
using AutoMapper;
using MethodologyMain.Logic.Models;
using MethodTeams.DTO;

namespace MethodTeams.Services
{
    public class TeamService: ITeamService
    {
        private readonly ITeamRepository teamRepo;
        private readonly ITeamValidationService validation;
        private readonly IMapper mapper;
        public TeamService(
            ITeamRepository teamRepo,
            ITeamValidationService validation,
            IMapper mapper)
        {
            this.teamRepo = teamRepo;
            this.validation = validation;
            this.mapper = mapper;
        }

        // Создание новой команды
        public async Task<TeamEntity> CreateTeamAsync(
            string name, 
            string description, 
            Guid captainId, 
            Guid HackathonId,
            CancellationToken token
            )
        {
            // Проверка, что у пользователя нет другой команды для этого 
            await validation.CheckUserNotInAnyTeamForHackathonAsync(captainId, HackathonId, token);
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
            await teamRepo.AddAsync(team, token);
            return team;
        }

        // Удаление команды (только капитаном или администратором)
        public async Task DeleteTeamAsync(
            Guid teamId, 
            Guid requestingUserId,
            CancellationToken token,
            bool isAdmin = false
            )
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            await teamRepo.RemoveTeamAsync(teamId, token);
        }

        // Добавление пользователя в команду
        public async Task AddUserToTeamAsync(
            Guid teamId, 
            Guid userId, 
            Guid requestingUserId,
            CancellationToken token
            )
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            Guid hackathonId = (Guid)await teamRepo.GetHackathonIdAsync(teamId, token);
            await validation.CheckUserIsCaptainAsync(teamId, requestingUserId, token);
            await validation.CheckUserNotInTeamAsync(userId, teamId, token);
            await validation.CheckUserNotInAnyTeamForHackathonAsync(userId, hackathonId, token);
            await teamRepo.AddMemberAsync(userId, teamId, token);
        }

        // Удаление пользователя из команды
        public async Task RemoveUserFromTeamAsync(
            Guid teamId, 
            Guid userId, 
            Guid requestingUserId,
            CancellationToken token
            )
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            // Проверка прав: капитан может удалить любого, участник - только себя
            await validation.CheckUserIsCaptainOrRequestedAsync(teamId, userId, requestingUserId, token);
            // Нельзя удалить капитана
            await validation.CheckCaptainKick(teamId, userId, token);
            await validation.CheckUserInTeamAsync(userId, teamId, token);
            await teamRepo.RemoveMemberAsync(userId, teamId, token);
        }
        // Передача прав капитана
        public async Task TransferCaptainRightsAsync(
            Guid teamId, 
            Guid newCaptainId, 
            Guid currentCaptainId,
            CancellationToken token
            )
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            await validation.CheckUserIsCaptainAsync(teamId, currentCaptainId, token);
            await validation.CheckUserInTeamAsync(newCaptainId, teamId, token);
            await teamRepo.TransferCaptainAsync(newCaptainId, teamId, token);
        }
        // Получение информации о команде по ID
        public async Task<TeamEntity> GetTeamByIdAsync(Guid teamId, CancellationToken token)
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            return await teamRepo.GetByIdAsync(teamId, token);
        }
        // Получение списка участников команды
        public async Task<List<Guid>> GetTeamMembersAsync(Guid teamId, CancellationToken token)
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            return await teamRepo.GetTeamMembersAsync(teamId, token);
        }

        // Получение списка команд
        public async Task<List<TeamInfoDto>> GetTeamAllAsync(CancellationToken token)
        {
            
            var teams = await teamRepo.GetAllAsync(token);
            return mapper.Map<List<TeamInfoDto>>(teams);
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
