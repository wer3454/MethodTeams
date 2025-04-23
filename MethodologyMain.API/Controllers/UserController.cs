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
    public class UserController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly ILogQueueService logQueueService;
        public UserController(ITeamService teamService, ILogQueueService logQueueService)
        {
            this.teamService = teamService;
            this.logQueueService = logQueueService;
        }
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamDto dto, CancellationToken token)
        {
            _ = logQueueService.SendLogEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            Guid currentUserId = GetCurrentUserId(); // Получение ID текущего пользователя из токена
            var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(team);
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse("1");
        }
    }
}
