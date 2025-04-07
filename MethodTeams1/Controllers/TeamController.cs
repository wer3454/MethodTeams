using MethodTeams.DTO;
using MethodTeams.Models;
using MethodTeams.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MethodTeams.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // Создание команды
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamDto dto)
        {
            try
            {
                int currentUserId = GetCurrentUserId(); // Получение ID текущего пользователя из токена
                var team = await _teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId);
                return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Получение информации о команде
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            try
            {
                var team = await _teamService.GetTeamByIdAsync(id);
                return Ok(team);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Удаление команды
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            try
            {
                int currentUserId = GetCurrentUserId();
                bool isAdmin = User.IsInRole("Admin"); // Проверка роли администратора

                await _teamService.DeleteTeamAsync(id, currentUserId, isAdmin);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // Добавление пользователя в команду
        [HttpPost("{id}/members")]
        //[Authorize]
        public async Task<ActionResult> AddMember(int id, [FromBody] AddUserDto dto)
        {
            try
            {
                int currentUserId = GetCurrentUserId();
                await _teamService.AddUserToTeamAsync(id, dto.UserId, currentUserId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Удаление пользователя из команды
        [HttpDelete("{id}/members/{userId}")]
        //[Authorize]
        public async Task<ActionResult> RemoveMember(int id, int userId)
        {
            try
            {
                int currentUserId = GetCurrentUserId();
                await _teamService.RemoveUserFromTeamAsync(id, userId, currentUserId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Получение списка участников команды
        [HttpGet("{id}/members")]
        public async Task<ActionResult<List<int>>> GetTeamMembers(int id)
        {
            try
            {
                var members = await _teamService.GetTeamMembersAsync(id);
                return Ok(members);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Передача прав капитана
        [HttpPut("{id}/captain")]
        //[Authorize]
        public async Task<ActionResult> TransferCaptainRights(int id, [FromBody] AddUserDto dto)
        {
            try
            {
                int currentUserId = GetCurrentUserId();
                await _teamService.TransferCaptainRightsAsync(id, dto.UserId, currentUserId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Получение списка команд для события
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<Team>>> GetTeamsByEvent(int eventId)
        {
            var teams = await _teamService.GetTeamsByEventIdAsync(eventId);
            return Ok(teams);
        }

        // Получение команды пользователя для конкретного события
        [HttpGet("event/{eventId}/user")]
        //[Authorize]
        public async Task<ActionResult<Team>> GetUserTeamForEvent(int eventId)
        {
            try
            {
                int currentUserId = GetCurrentUserId();
                var team = await _teamService.GetUserTeamForEventAsync(currentUserId, eventId);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Вспомогательный метод для получения ID текущего пользователя
        //private int GetCurrentUserId()
        //{
        //    // В реальном приложении здесь нужно извлечь ID из токена авторизации
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    return int.Parse(userIdClaim.Value);
        //}
        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}
