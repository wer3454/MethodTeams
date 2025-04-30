using AutoMapper;
using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Application.Interface;
using MethodologyMain.Persistence.Interfaces;

namespace MethodologyMain.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        // Создание нового пользователя
        public async Task<GetUserDto> CreateUserAsync(GetUserDto dto, CancellationToken token)
        {
            var user = mapper.Map<UserMainEntity>(dto);
            await userRepo.AddAsync(user, token);
            return dto;
        }
        public async Task DeleteUserAsync(Guid userId, CancellationToken token)
        {
            await userRepo.RemoveAsync(userId, token);
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }
        public async Task UpdateUserAsync(GetUserDto dto, CancellationToken token)
        {
            var user = mapper.Map<UserMainEntity>(dto);
            await userRepo.UpdateAsync(user, token);
            // await validation.CheckTeamExistsAsync(teamId, token);
            // await validation.CheckUserIsCaptainOrAdminAsync(teamId, requestingUserId, isAdmin, token);
            // await userRepo.RemoveAsync(teamId, token);
        }

        // Получение информации о пользователе по ID
        public async Task<GetUserDto> GetUserByIdAsync(Guid userId, CancellationToken token)
        {
            // await validation.CheckTeamExistsAsync(teamId, token);
            var user = await userRepo.GetByIdAsync(userId, token);
            return mapper.Map<GetUserDto>(user);
        }

        // Получение списка пользователей
        public async Task<List<GetUserDto>> GetUsersAllAsync(CancellationToken token)
        {

            var users = await userRepo.GetAllAsync(token);
            return mapper.Map<List<GetUserDto>>(users);
        }
    }
}
