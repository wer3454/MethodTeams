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
        private readonly IUserService userService;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public UserController(IUserService userService, IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService)
        {
            this.userService = userService;
            this.logPublishService = logPublishService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUserDto>>> GetUsers(CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var users = await userService.GetUsersAllAsync(token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetUserDto>>> GetUsersById(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/User/id was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await userService.GetUserByIdAsync(id, token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<GetUserDto>> CreateUser([FromBody] GetUserDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await userService.CreateUserAsync(dto, token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] GetUserDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "PUT api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            await userService.UpdateUserAsync(dto, token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok();
        }
    }
}
