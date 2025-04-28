using AuthMetodology.Infrastructure.Models;
using MethodologyMain.Infrastructure.Models;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMqListener.Abstract;
using RabbitMqModel.Models;
using RabbitMqPublisher.Interface;
using Serilog.Events;
using System.Text.Json;

namespace MethodologyMain.Infrastructure.Listeners
{
    public class RabbitMqUserRegisterListener : RabbitMqListenerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService;
        public RabbitMqUserRegisterListener(IRabbitMqPublisherBase<RabbitMqLogPublish> logPublishService, IServiceProvider serviceProvider, IOptions<RabbitMqOptions> options) : base(options)
        {
            this.logPublishService = logPublishService;
            this.serviceProvider = serviceProvider;
        }

        protected override string QueueName => "RegisterUserQueue";

        public override async Task ProcessMessageAsync(string message)
        {
            var data = JsonSerializer.Deserialize<RabbitMqUserRegisterRecieve>(message)!;
            _ = logPublishService.SendEventAsync(new RabbitMqLogPublish
            {
                ServiceName = "Main service",
                LogLevel = LogEventLevel.Information,
                Message = "Method ProcessMessageAsync to register a user was called",
                TimeStamp = DateTime.UtcNow
            });
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                await context.AddAsync(new UserMainEntity
                {
                    Id = data.UserId,
                    UserName = data.UserName,
                    Email = data.Email,                    
                });
            }
            
        }
    }
}
