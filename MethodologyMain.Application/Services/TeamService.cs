using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Application.Interface;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Application.Exceptions;
using MethodTeams.Models;
using AutoMapper;
using MethodologyMain.Logic.Models;
using MethodologyMain.Application.DTO;

namespace MethodTeams.Services
{
    public class TeamService: ITeamService
    {
        private readonly ITeamRepository teamRepo;
        private readonly ITagRepository tagRepo;
        private readonly ITeamValidationService validation;
        private readonly IMapper mapper;
        public TeamService(
            ITeamRepository teamRepo,
            ITagRepository tagRepo,
            ITeamValidationService validation,
            IMapper mapper)
        {
            this.teamRepo = teamRepo;
            this.tagRepo = tagRepo;
            this.validation = validation;
            this.mapper = mapper;
        }

        // Создание новой команды
        public async Task<GetTeamDto> CreateTeamAsync(
            CreateTeamDto dto,
            Guid captainId, 
            CancellationToken token
            )
        {
            // Проверка, что у пользователя нет другой команды для этого 
            await validation.CheckUserNotInAnyTeamForHackathonAsync(captainId, dto.HackathonId, token);
            // Создание команды
            var team = new TeamEntity
            {
                
                Id = Guid.NewGuid(),
                HackathonId = dto.HackathonId,
                Name = dto.Name,
                Description = dto.Description,
                CaptainId = captainId,
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
            await tagRepo.AddTeamTags(team.Id, dto.Tags, token);
            return mapper.Map<GetTeamDto>(team);
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

        public async Task UpdateTeamAsync(
            UpdateTeamDto team,
            Guid requestingUserId,
            CancellationToken token,
            bool isAdmin = false
            )
        {
            await validation.CheckTeamExistsAsync(team.Id, token);
            await validation.CheckUserIsCaptainOrAdminAsync(team.Id, requestingUserId, isAdmin, token);
            var teamEntity = await teamRepo.GetByIdAsync(team.Id, token);
            if (string.IsNullOrEmpty(team.Description) && teamEntity.Description != team.Description)
                teamEntity.Description = team.Description;
            if (string.IsNullOrEmpty(team.Name) && teamEntity.Name != team.Name)
                teamEntity.Name = team.Name;
            if (team.HackathonId != null && teamEntity.HackathonId != team.HackathonId) 
                teamEntity.HackathonId = (Guid)team.HackathonId;
            await teamRepo.UpdateTeamAsync(teamEntity, token);
            
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
        public async Task<GetTeamDto> GetTeamByIdAsync(Guid teamId, CancellationToken token)
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            var team = await teamRepo.GetTeamAsync(teamId, token);
            return mapper.Map<GetTeamDto>(team);
        }
        // Получение списка участников команды
        public async Task<List<string>> GetTeamMembersAsync(Guid teamId, CancellationToken token)
        {
            await validation.CheckTeamExistsAsync(teamId, token);
            return await teamRepo.GetTeamMembersAsync(teamId, token);
        }

        // Получение списка команд
        public async Task<List<GetTeamDto>> GetTeamAllAsync(CancellationToken token)
        {

            var teams = await teamRepo.GetTeamsAllAsync(token);
            //return mapper.Map<List<TeamInfoDto>>(teams);
            return mapper.ProjectTo<GetTeamDto>(teams.AsQueryable()).ToList();
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
