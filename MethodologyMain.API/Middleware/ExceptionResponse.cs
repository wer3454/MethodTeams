using System.Net;

namespace MethodologyMain.API.Middleware
{
    public record ExceptionResponse(string Message, HttpStatusCode Code);
}
