using MethodologyMain.Application.DTO;
using MethodTeams.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MethodologyMain.Application.Interface;
using AuthMetodology.Infrastructure.Interfaces;
using AuthMetodology.Infrastructure.Models;
using Serilog.Events;
namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogQueueService logQueueService;
        public TeamController(ITeamService teamService, ILogQueueService logQueueService)
        {
            _teamService = teamService;
            this.logQueueService = logQueueService;
        }

        // Создание команды
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamDto dto)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId(); // Получение ID текущего пользователя из токена
            var team = await _teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId);
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            //try
            //{
                
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // Получение информации о команде
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(Guid id)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/id was called",
                TimeStamp = DateTime.UtcNow
            });
            var team = await _teamService.GetTeamByIdAsync(id);
            return Ok(team);
            //try
            //{
                
            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
        }

        // Удаление команды
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult> DeleteTeam(Guid id)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "DELETE api/Team/id was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId();
            bool isAdmin = User.IsInRole("Admin"); // Проверка роли администратора

            await _teamService.DeleteTeamAsync(id, currentUserId, isAdmin);
            return NoContent();
            //try
            //{

            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return Forbid(ex.Message);
            //}
        }

        // Добавление пользователя в команду
        [HttpPost("{id}/members")]
        //[Authorize]
        public async Task<ActionResult> AddMember(Guid id, [FromBody] AddUserDto dto)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team/id/members was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId();
            await _teamService.AddUserToTeamAsync(id, dto.UserId, currentUserId);
            return NoContent();
            //try
            //{

            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return Forbid(ex.Message);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // Удаление пользователя из команды
        [HttpDelete("{id}/members/{userId}")]
        //[Authorize]
        public async Task<ActionResult> RemoveMember(Guid id, Guid userId)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "DELETE api/Team/id/members/userId was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId();
            await _teamService.RemoveUserFromTeamAsync(id, userId, currentUserId);
            return NoContent();
            //try
            //{
                
            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return Forbid(ex.Message);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // Получение списка участников команды
        [HttpGet("{id}/members")]
        public async Task<ActionResult<List<int>>> GetTeamMembers(Guid id)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/id/members was called",
                TimeStamp = DateTime.UtcNow
            });
            var members = await _teamService.GetTeamMembersAsync(id);
            return Ok(members);
            //try
            //{
                
            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
        }

        // Передача прав капитана
        [HttpPut("{id}/captain")]
        //[Authorize]
        public async Task<ActionResult> TransferCaptainRights(Guid id, [FromBody] AddUserDto dto)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "PUT api/Team/id/captain was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId();
            await _teamService.TransferCaptainRightsAsync(id, dto.UserId, currentUserId);
            return NoContent();
            //try
            //{

            //}
            //catch (KeyNotFoundException)
            //{
            //    return NotFound();
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return Forbid(ex.Message);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // Получение списка команд для события
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<Team>>> GetTeamsByEvent(Guid eventId)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/event/eventId was called",
                TimeStamp = DateTime.UtcNow
            });
            var teams = await _teamService.GetTeamsByEventIdAsync(eventId);
            return Ok(teams);
        }

        // Получение команды пользователя для конкретного события
        [HttpGet("event/{eventId}/user")]
        //[Authorize]
        public async Task<ActionResult<Team>> GetUserTeamForEvent(Guid eventId)
        {
            await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/event/eventId/user was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId();
            var team = await _teamService.GetUserTeamForEventAsync(currentUserId, eventId);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);

            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        // Вспомогательный метод для получения ID текущего пользователя
        //private int GetCurrentUserId()
        //{
        //    // В реальном приложении здесь нужно извлечь ID из токена авторизации
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    return int.Parse(userIdClaim.Value);
        //}
        private Guid GetCurrentUserId()
        {
            return Guid.Parse("1");
        }
    }
}
