using MethodTeams.Data;
using MethodTeams.Models;
using MethodTeams.Interface;
using Microsoft.EntityFrameworkCore;

namespace MethodTeams.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamDbContext _context;

        public TeamService(TeamDbContext context)
        {
            _context = context;
        }

        // Создание новой команды
        public async Task<Team> CreateTeamAsync(string name, string description, int captainId, int eventId)
        {
            // Проверка, что у пользователя нет другой команды для этого события
            var existingTeam = await _context.TeamMembers
                .Include(tm => tm.Team)
                .Where(tm => tm.UserId == captainId && tm.Team.EventId == eventId)
                .FirstOrDefaultAsync();

            if (existingTeam != null)
            {
                throw new InvalidOperationException("Пользователь уже состоит в команде для данного события");
            }

            // Создание команды
            var team = new Team
            {
                Name = name,
                Description = description,
                CaptainId = captainId,
                EventId = eventId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            // Добавление капитана как участника команды
            var member = new TeamMember
            {
                TeamId = team.Id,
                UserId = captainId,
                JoinedAt = DateTime.UtcNow
            };

            await _context.TeamMembers.AddAsync(member);
            await _context.SaveChangesAsync();

            return team;
        }

        // Удаление команды (только капитаном или администратором)
        public async Task DeleteTeamAsync(int teamId, int requestingUserId, bool isAdmin = false)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            if (team.CaptainId != requestingUserId && !isAdmin)
            {
                throw new UnauthorizedAccessException("Только капитан команды или администратор может удалить команду");
            }

            // Удаление связанных участников
            var members = await _context.TeamMembers.Where(m => m.TeamId == teamId).ToListAsync();
            _context.TeamMembers.RemoveRange(members);

            // Удаление команды
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        // Добавление пользователя в команду
        public async Task AddUserToTeamAsync(int teamId, int userId, int requestingUserId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            if (team.CaptainId != requestingUserId)
            {
                throw new UnauthorizedAccessException("Только капитан команды может добавлять участников");
            }

            // Проверка, что пользователь не состоит в команде этого события
            var existingMembership = await _context.TeamMembers
                .Include(tm => tm.Team)
                .Where(tm => tm.UserId == userId && tm.Team.EventId == team.EventId)
                .FirstOrDefaultAsync();

            if (existingMembership != null)
            {
                throw new InvalidOperationException("Пользователь уже состоит в команде для данного события");
            }

            // Проверка, что пользователь не является уже членом этой команды
            var existingTeamMember = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId && tm.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingTeamMember != null)
            {
                throw new InvalidOperationException("Пользователь уже состоит в этой команде");
            }

            // Добавление пользователя в команду
            var member = new TeamMember
            {
                TeamId = teamId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            };

            await _context.TeamMembers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        // Удаление пользователя из команды
        public async Task RemoveUserFromTeamAsync(int teamId, int userId, int requestingUserId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            // Проверка прав: капитан может удалить любого, участник - только себя
            bool canRemove = (team.CaptainId == requestingUserId) || (userId == requestingUserId);

            if (!canRemove)
            {
                throw new UnauthorizedAccessException("Недостаточно прав для удаления участника");
            }

            // Нельзя удалить капитана
            if (userId == team.CaptainId)
            {
                throw new InvalidOperationException("Капитан не может быть удален из команды. Сначала передайте права капитана другому участнику.");
            }

            var member = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId && tm.UserId == userId)
                .FirstOrDefaultAsync();

            if (member == null)
            {
                throw new KeyNotFoundException("Пользователь не найден в команде");
            }

            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();
        }

        // Передача прав капитана
        public async Task TransferCaptainRightsAsync(int teamId, int newCaptainId, int currentCaptainId)
        {
            var team = await _context.Teams.FindAsync(teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            if (team.CaptainId != currentCaptainId)
            {
                throw new UnauthorizedAccessException("Только текущий капитан может передать права капитана");
            }

            // Проверка, что новый капитан является участником команды
            var newCaptainMember = await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId && tm.UserId == newCaptainId)
                .FirstOrDefaultAsync();

            if (newCaptainMember == null)
            {
                throw new InvalidOperationException("Новый капитан должен быть участником команды");
            }

            // Передача прав
            team.CaptainId = newCaptainId;
            await _context.SaveChangesAsync();
        }

        // Получение информации о команде по ID
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            return team;
        }

        // Получение списка команд для конкретного события
        public async Task<List<Team>> GetTeamsByEventIdAsync(int eventId)
        {
            return await _context.Teams
                .Where(t => t.EventId == eventId)
                .ToListAsync();
        }

        // Получение списка участников команды
        public async Task<List<int>> GetTeamMembersAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
            {
                throw new KeyNotFoundException("Команда не найдена");
            }

            return team.Members.Select(m => m.UserId).ToList();
        }

        // Проверка, является ли пользователь капитаном команды
        public async Task<bool> IsUserTeamCaptainAsync(int teamId, int userId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            return team != null && team.CaptainId == userId;
        }

        // Получение команды пользователя для конкретного события
        public async Task<Team> GetUserTeamForEventAsync(int userId, int eventId)
        {
            return await _context.TeamMembers
                .Where(tm => tm.UserId == userId)
                .Include(tm => tm.Team)
                .Where(tm => tm.Team.EventId == eventId)
                .Select(tm => tm.Team)
                .FirstOrDefaultAsync();
        }
    }
}
