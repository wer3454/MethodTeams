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
    [Route("api/main/[controller]")]
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

        // Получение списка команд
        [HttpGet]
        public async Task<ActionResult<List<GetTeamDto>>> GetTeamsAll(CancellationToken token)
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
        }

        // Получение информации о команде
        [HttpGet("hackathon/{hackId}")]
        public async Task<ActionResult<List<GetTeamDto>>> GetTeamsForHackathon(Guid hackId, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Team/hackathon/id was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            var teams = await teamService.GetTeamForHackathonAsync(hackId, token);
            return Ok(teams);
        }

        // Создание команды
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<CreateTeamDto>> CreateTeam([FromBody] CreateTeamDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            // Получение ID текущего пользователя из токена
            var team = await teamService.CreateTeamAsync(dto, dto.CreatedBy, token);
            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }

        // Получение информации о команде
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTeamDto>> GetTeamById(Guid id, CancellationToken token)
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
        }

        // Добавление пользователя в команду
        [HttpPost("{id}/users/{userId}")]
        //[Authorize]
        public async Task<ActionResult> JoinMember(Guid id, Guid userId, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/Team/id/users/userId was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            await teamService.JoinUserToTeamAsync(id, userId, token);
            return NoContent();
        }

        // Удаление пользователя из команды
        [HttpDelete("{id}/users/{userId}")]
        //[Authorize]
        public async Task<ActionResult> LeaveMember(Guid id, Guid userId, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "DELETE api/Team/id/users/userId was called",
                TimeStamp = DateTime.UtcNow
            }, token);
            await teamService.LeaveUserFromTeamAsync(id, userId, token);
            return NoContent();
        }

        //[HttpPut]
        ////[Authorize]
        //public async Task<ActionResult> UpdateTeam([FromBody] UpdateTeamDto dto, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "Update api/Team/id was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    Guid currentUserId = GetCurrentUserId();
        //    await teamService.UpdateTeamAsync(dto, currentUserId, token);
        //    return NoContent();
        //}

        //// Удаление команды
        //[HttpDelete("{id}")]
        ////[Authorize]
        //public async Task<ActionResult> DeleteTeam(Guid id, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "DELETE api/Team/id was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    Guid currentUserId = GetCurrentUserId();
        //    bool isAdmin = User.IsInRole("Admin"); // Проверка роли администратора

        //    await teamService.DeleteTeamAsync(id, currentUserId, token, isAdmin);
        //    return NoContent();
        //}

        //// Добавление пользователя в команду
        //[HttpPost("{id}/users")]
        ////[Authorize]
        //public async Task<ActionResult> AddMember(Guid id, [FromBody] AddUserDto dto, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "POST api/Team/id/members was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    Guid currentUserId = GetCurrentUserId();
        //    await teamService.AddUserToTeamAsync(id, dto.UserId, currentUserId, token);
        //    return NoContent();
        //}

        //// Удаление пользователя из команды
        //[HttpDelete("{id}/users/{userId}")]
        ////[Authorize]
        //public async Task<ActionResult> RemoveMember(Guid id, Guid userId, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "DELETE api/Team/id/members/userId was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    Guid currentUserId = GetCurrentUserId();
        //    await teamService.RemoveUserFromTeamAsync(id, userId, currentUserId, token);
        //    return NoContent();
        //}

        //// Получение списка участников команды
        //[HttpGet("{id}/users")]
        //public async Task<ActionResult<List<string>>> GetTeamMembers(Guid id, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "GET api/Team/id/members was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    var members = await teamService.GetTeamMembersAsync(id, token);
        //    return Ok(members);
        //}

        //// Передача прав капитана
        //[HttpPut("{id}/captain")]
        ////[Authorize]
        //public async Task<ActionResult> TransferCaptainRights(Guid id, [FromBody] AddUserDto dto, CancellationToken token)
        //{
        //    _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
        //    {
        //        ServiceName = "Main service",
        //        LogLevel = LogEventLevel.Information,
        //        Message = "PUT api/Team/id/captain was called",
        //        TimeStamp = DateTime.UtcNow
        //    }, token);
        //    Guid currentUserId = GetCurrentUserId();
        //    await teamService.TransferCaptainRightsAsync(id, dto.UserId, currentUserId, token);
        //    return NoContent();
        //}
        // Вспомогательный метод для получения ID текущего пользователя
        //private int GetCurrentUserId()
        //{
        //    // В реальном приложении здесь нужно извлечь ID из токена авторизации
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    return int.Parse(userIdClaim.Value);
        //}
        //private Guid GetCurrentUserId()
        //{
        //    return Guid.Parse("d5a492ca-9611-4fda-9ace-10ec38bb4d48");
        //}
    }
}
