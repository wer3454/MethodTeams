using AuthMetodology.Infrastructure.Interfaces;
using MethodologyMain.Application.Exceptions;
using Serilog.Events;

namespace MethodologyMain.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogQueueService logQueueService;
        public ExceptionMiddleware(RequestDelegate next, ILogQueueService logQueueService)
        {
            this.next = next;
            this.logQueueService = logQueueService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await logQueueService.SendLogEventAsync(new AuthMetodology.Infrastructure.Models.RabbitMqLogPublish {
                    ServiceName = "Main service",
                    LogLevel = LogEventLevel.Error,
                    Message = $"Exception was thrown.\nMessage: {ex.Message}\nSource: {ex.Source}",
                    TimeStamp = DateTime.UtcNow
                });
                HandleException(ex,context);
            }
        }

        private static void HandleException(Exception ex, HttpContext context)
        {
            ExceptionResponse response = ex switch
            {
                MemberAlreadyInTeamException _ => new ExceptionResponse("Пользователь уже состоит в команде для данного события", System.Net.HttpStatusCode.Conflict),
                TeamNotFoundException _ => new ExceptionResponse("Команда не найдена", System.Net.HttpStatusCode.Conflict),
                UnauthorizedAccessException _ => new ExceptionResponse("У вас не таких прав", System.Net.HttpStatusCode.Unauthorized),
                InvalidOperationException _ => new ExceptionResponse(ex.Message, System.Net.HttpStatusCode.Conflict),
                UserNotFoundException _ => new ExceptionResponse("Пользователь не найден", System.Net.HttpStatusCode.Conflict),
                UserNotInTeamException _ => new ExceptionResponse("Пользователь не в команде", System.Net.HttpStatusCode.Conflict),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
