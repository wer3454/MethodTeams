using AuthMetodology.Infrastructure.Models;

namespace AuthMetodology.Infrastructure.Interfaces
{
    public interface ILogQueueService
    {
        Task SendLogEventAsync(RabbitMqLogPublish message);
    }
}
