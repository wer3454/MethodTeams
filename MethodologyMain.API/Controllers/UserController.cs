﻿using MethodologyMain.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using MethodologyMain.Application.Interface;
using AuthMetodology.Infrastructure.Models;
using Serilog.Events;
using RabbitMqPublisher.Interface;
using Microsoft.AspNetCore.Cors;

namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [EnableCors("AllowFrontend")]
    [Route("api/main/[controller]")]
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
                Message = "GET api/main/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var users = await userService.GetUsersAllAsync(token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUsersById([FromRoute] Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/main/User was called",
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
                Message = "POST api/main/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await userService.CreateUserAsync(dto, token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<GetUserDto>> UpdateUser([FromBody] GetUserDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "PUT api/main/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await userService.UpdateUserAsync(dto, token);
            //var team = await teamService.CreateTeamAsync(dto.Name, dto.Description, currentUserId, dto.EventId, token);
            return Ok(user);
        }
    }
}
