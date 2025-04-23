using MethodologyMain.Application.DTO;
using MethodTeams.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MethodologyMain.Application.Interface;
using AuthMetodology.Infrastructure.Models;
using Serilog.Events;
using RabbitMqPublisher.Interface;

namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public UserController(ITeamService teamService, IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService)
        {
            this.teamService = teamService;
            this.logPublishService = logPublishService;
        }
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateTeamDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
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
