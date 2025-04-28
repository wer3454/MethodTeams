using AuthMetodology.Infrastructure.Models;
using Microsoft.Extensions.Options;
using RabbitMqModel.Models;
using RabbitMqPublisher.Abstract;

namespace MethodologyMain.Infrastructure.Services
{
    public class LogQueueService : RabbitMqPublisherBase<RabbitMqLogPublish>
    {
        public LogQueueService(IOptions<RabbitMqOptions> options) : base(options) { }

        public override string QueueName => "LogQueue";

        public override async Task SendEventAsync(RabbitMqLogPublish message, CancellationToken cancellationToken = default)
        {
            await SendMessageAsync(message, QueueName, cancellationToken);
        }
    }
}
