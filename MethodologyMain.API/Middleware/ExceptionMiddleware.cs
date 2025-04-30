using AuthMetodology.Infrastructure.Models;
using MethodologyMain.Application.Exceptions;
using RabbitMqPublisher.Interface;
using Serilog.Events;

namespace MethodologyMain.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRabbitMqPublisherBase<RabbitMqLogPublish> logService;
        public ExceptionMiddleware(RequestDelegate next, IRabbitMqPublisherBase<RabbitMqLogPublish> logService)
        {
            this.next = next;
            this.logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _ = logService.SendEventAsync(new RabbitMqLogPublish
                {
                    ServiceName = "Main service",
                    LogLevel = LogEventLevel.Error,
                    Message = $"Exception was thrown.\nMessage: {ex.Message}, {ex.InnerException}\nSource: {ex.Source}",
                    TimeStamp = DateTime.UtcNow
                });

                await HandleException(ex,context);
            }
        }

        private static async Task HandleException(Exception ex, HttpContext context)
        {
            ExceptionResponse response = ex switch
            {
                MemberAlreadyInTeamException _ => new ExceptionResponse("Пользователь уже состоит в команде для данного события", System.Net.HttpStatusCode.Conflict),
                TeamNotFoundException _ => new ExceptionResponse("Команда не найдена", System.Net.HttpStatusCode.Conflict),
                UnauthorizedAccessException _ => new ExceptionResponse("У вас не таких прав", System.Net.HttpStatusCode.Unauthorized),
                InvalidOperationException _ => new ExceptionResponse(ex.Message, System.Net.HttpStatusCode.Conflict),
                UserNotFoundException _ => new ExceptionResponse("Пользователь не найден", System.Net.HttpStatusCode.Conflict),
                UserNotInTeamException _ => new ExceptionResponse("Пользователь не в команде", System.Net.HttpStatusCode.Conflict),
                _ => new ExceptionResponse(ex.Message, System.Net.HttpStatusCode.InternalServerError),
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.Code;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
