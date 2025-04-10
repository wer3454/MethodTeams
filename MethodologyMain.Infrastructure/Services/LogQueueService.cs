using AuthMetodology.Infrastructure.Interfaces;
using AuthMetodology.Infrastructure.Models;

namespace MethodologyMain.Infrastructure.Services
{
    public class LogQueueService : ILogQueueService
    {
        private readonly IRabbitMqService rabbitMqService;
        private readonly string QueueName = "LogQueue";
        public LogQueueService(IRabbitMqService rabbitMqService) => this.rabbitMqService = rabbitMqService;

        public async Task SendLogEventAsync(RabbitMqLogPublish message)
        {
            await rabbitMqService.SendMessageAsync(message, QueueName);
        }
    }
}
