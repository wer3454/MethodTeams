using MethodologyMain.Application.DTO;
using MethodTeams.Models;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
using MethodologyMain.Application.Interface;
using AuthMetodology.Infrastructure.Models;
using Serilog.Events;
using RabbitMqPublisher.Interface;
namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public TeamController(ITeamService teamService, IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService)
        {
            this.teamService = teamService;
            this.logPublishService = logPublishService;
        }

        //Тестовый эндпоинт
        [HttpGet("dummy-data")]
        public async Task<IActionResult> GetDummyData(CancellationToken cancellationToken)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team/dummy-data was called",
                TimeStamp = DateTime.UtcNow
            }, cancellationToken);
            string data = "Dataaa";
            return Ok(data);
        }

        // Создание команды
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            Guid currentUserId = GetCurrentUserId(); // Получение ID текущего пользователя из токена
            var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
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
        public async Task<ActionResult<Team>> GetTeam(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/id was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            var team = await teamService.GetTeamByIdAsync(id, token);
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
        public async Task<ActionResult> DeleteTeam(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "DELETE api/Team/id was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            Guid currentUserId = GetCurrentUserId();
            bool isAdmin = User.IsInRole("Admin"); // Проверка роли администратора

            await teamService.DeleteTeamAsync(id, currentUserId, token, isAdmin);
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
        [HttpPost("{id}/users")]
        //[Authorize]
        public async Task<ActionResult> AddMember(Guid id, [FromBody] AddUserDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team/id/members was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            Guid currentUserId = GetCurrentUserId();
            await teamService.AddUserToTeamAsync(id, dto.UserId, currentUserId, token);
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
        [HttpDelete("{id}/users/{userId}")]
        //[Authorize]
        public async Task<ActionResult> RemoveMember(Guid id, Guid userId, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "DELETE api/Team/id/members/userId was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            Guid currentUserId = GetCurrentUserId();
            await teamService.RemoveUserFromTeamAsync(id, userId, currentUserId, token);
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
        [HttpGet("{id}/users")]
        public async Task<ActionResult<List<int>>> GetTeamMembers(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/id/members was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            var members = await teamService.GetTeamMembersAsync(id, token);
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
        public async Task<ActionResult> TransferCaptainRights(Guid id, [FromBody] AddUserDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "PUT api/Team/id/captain was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            Guid currentUserId = GetCurrentUserId();
            await teamService.TransferCaptainRightsAsync(id, dto.UserId, currentUserId, token);
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

        // Получение списка команд
        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetTeamsAll(CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            var teams = await teamService.GetTeamAllAsync(token);
            return Ok(teams);
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

        //// Получение списка команд для события
        //[HttpGet("event/{eventId}")]
        //public async Task<ActionResult<List<Team>>> GetTeamsByEvent(Guid eventId)
        //{
        //    await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "GET api/Team/event/eventId was called",
        //        TimeStamp = DateTime.UtcNow
        //    });
        //    var teams = await _teamService.GetTeamsByEventIdAsync(eventId);
        //    return Ok(teams);
        //}

        //// Получение команды пользователя для конкретного события
        //[HttpGet("event/{eventId}/user")]
        ////[Authorize]
        //public async Task<ActionResult<Team>> GetUserTeamForEvent(Guid eventId)
        //{
        //    await logQueueService.SendLogEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "GET api/Team/event/eventId/user was called",
        //        TimeStamp = DateTime.UtcNow
        //    });
        //    Guid currentUserId = GetCurrentUserId();
        //    var team = await _teamService.GetUserTeamForEventAsync(currentUserId, eventId);

        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(team);

        //try
        //{

        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}
        //}

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
