using AuthMetodology.Infrastructure.Interfaces;
using AuthMetodology.Infrastructure.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;

namespace MethodologyMain.Infrastructure.Services
{
    public class RabbitMqService : IRabbitMqService, IAsyncDisposable
    {
        private IConnection? connection;
        private bool disposed;
        private SemaphoreSlim connectionLock = new SemaphoreSlim(1,1);
        private readonly RabbitMqOptions options;
        public RabbitMqService(IOptions<RabbitMqOptions> options)
        {
            this.options = options.Value;
        }
        public async ValueTask DisposeAsync()
        {
            if (disposed)
                return;

            disposed = true;
            if (connection?.IsOpen == true)
            {
                await connection.CloseAsync();
            }
            connection?.Dispose();
            connectionLock.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task<IConnection> GetConnectionAsync()
        {
            await connectionLock.WaitAsync();
            try
            {
                if (connection?.IsOpen == true) return connection;

                connection?.Dispose();
                var factory = new ConnectionFactory() 
                {
                    HostName = options.Host,
                    Port = options.Port
                };
                connection = await factory.CreateConnectionAsync();
                return connection;
            }
            finally
            {
                connectionLock.Release();
            }
        }

        public async Task SendMessageAsync<T>(T message, string queueName) where T : class
        {
            connection = await GetConnectionAsync();

            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                var messageBody = JsonSerializer.SerializeToUtf8Bytes(message);

                await channel.BasicPublishAsync(exchange: "", routingKey: queueName, messageBody);
            };
        }
    }
}
