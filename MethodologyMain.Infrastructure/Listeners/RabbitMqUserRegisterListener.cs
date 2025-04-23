using MethodologyMain.Infrastructure.Models;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMqListener.Abstract;
using RabbitMqModel.Models;

namespace MethodologyMain.Infrastructure.Listeners
{
    public class RabbitMqUserRegisterListener : RabbitMqListenerBase<RabbitMqUserRegisterRecieve>
    {
        private readonly IUserRepository userRepository;
        public RabbitMqUserRegisterListener(IUserRepository userRepository, IOptions<RabbitMqOptions> options) : base(options)
        {
            this.userRepository = userRepository;
        }

        protected override string QueueName => "RegisterUserQueue";

        public override async Task ProcessMessageAsync(RabbitMqUserRegisterRecieve message)
        {
            await userRepository.AddAsync(new UserMainEntity
            {
                Id = message.UserId
            });
        }
    }
}
