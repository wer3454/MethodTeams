using AuthMetodology.Infrastructure.Models;
using MethodologyMain.Application.DTO;
using MethodologyMain.Application.Interface;
using MethodologyMain.Application.Services;
using MethodologyMain.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMqPublisher.Interface;
using Serilog.Events;

namespace MethodologyMain.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService orgService;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public OrganizationController(IOrganizationService orgService, IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService)
        {
            this.orgService = orgService;
            this.logPublishService = logPublishService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrganizationDto>>> GetOrganizations(CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Organization was called",
                TimeStamp = DateTime.UtcNow
            });
            var orgs = await orgService.GetOrganizationsAllAsync(token);
            return Ok(orgs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrganizationDto>> GetOrganizationById(Guid id, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "GET api/Organization/id was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await orgService.GetOrganizationByIdAsync(id, token);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrganizationDto>> CreateOrganization([FromBody] CreateOrganizationDto dto, CancellationToken token)
        {
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "POST api/User was called",
                TimeStamp = DateTime.UtcNow
            });
            var user = await orgService.CreateOrganizationAsync(dto, token);
            return Ok(user);
        }
    }
}
