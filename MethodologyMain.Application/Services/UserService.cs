using AutoMapper;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using MethodologyMain.Persistence.Interfaces;

namespace MethodologyMain.Application.Services
{
    class UserService
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        // Создание нового пользователя
        public async Task<UserMainEntity> CreateUserAsync(
            string UserName,
            string description,
            Guid captainId,
            DateTime BirthDate,
            CancellationToken token
            )
        {
            // Создание пользователя
            var user = new UserMainEntity
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                BirthDate = BirthDate
            };
            await userRepo.AddAsync(user, token);
            return user;
        }

        // Удаление команды (только капитаном или администратором)
        public async Task DeleteUserAsync(
            Guid userId,
            Guid requestingUserId,
            CancellationToken token,
            bool isAdmin = false
            )
        {
            throw new NotImplementedException();
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }
        public async Task UpdateUserAsync(
            Guid userId,
            Guid requestingUserId,
            CancellationToken token,
            bool isAdmin = false
            )
        {
            throw new NotImplementedException();
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }

        // Получение информации о команде по ID
        public async Task<UserMainEntity> GetUserByIdAsync(Guid userId, CancellationToken token)
        {
            // await validation.CheckTeamExistsAsync(teamId, token);
            return await userRepo.GetByIdAsync(userId, token);
        }

        // Получение списка команд
        public async Task<List<UserMain>> GetUsersAllAsync(CancellationToken token)
        {

            var teams = await userRepo.GetAllAsync(token);
            return mapper.Map<List<UserMain>>(teams);
        }
    }
}
