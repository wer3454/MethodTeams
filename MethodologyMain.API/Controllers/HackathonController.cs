using AuthMetodology.Infrastructure.Models;
using MethodologyMain.Application.DTO;
using MethodologyMain.Application.Interface;
using MethodologyMain.Application.Services;
using MethodologyMain.Logic.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RabbitMqPublisher.Interface;
using Serilog.Events;

namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackathonController : Controller
    {
        private readonly IHackathonService hackService;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public HackathonController(IHackathonService hackService, IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService)
        {
            this.hackService = hackService;
            this.logPublishService = logPublishService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetHackathonDto>>> GetHackathons(CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Hackathon was called",
                TimeStamp = DateTime.UtcNow
            });
            var hacks = await hackService.GetHacksAllAsync(token);
            return Ok(hacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetHackathonDto>>> GetUsersById(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Hackathon/id was called",
                TimeStamp = DateTime.UtcNow
            });
            var hack = await hackService.GetHackByIdAsync(id, token);
            return Ok(hack);
        }

        [HttpPost]
        public async Task<ActionResult<CreateHackathonDto>> CreateUser([FromBody] CreateHackathonDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await hackService.CreateHackAsync(dto, token);
            return Ok(user);
        }
    }
}
