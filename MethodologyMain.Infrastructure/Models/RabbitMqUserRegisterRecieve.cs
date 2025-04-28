using RabbitMqModel.Models;

namespace MethodologyMain.Infrastructure.Models
{
    public class RabbitMqUserRegisterRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string EventType => "RecieveUserRegister";
    }
}
