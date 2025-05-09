﻿using RabbitMqModel.Models;

namespace MethodologyMain.Infrastructure.Models
{
    public class RabbitMqUserRegisterRecieve : IEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string EventType => "RecieveUserRegister";
    }
}
