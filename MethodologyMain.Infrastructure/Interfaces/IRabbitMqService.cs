using RabbitMQ.Client;

namespace AuthMetodology.Infrastructure.Interfaces;

public interface IRabbitMqService
{
    //Task<IConnection> GetConnectionAsync();
    Task SendMessageAsync<T>(T message, string queueName)
        where T : class;
}