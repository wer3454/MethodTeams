using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;

namespace MethodologyMain.Application.Interface
{
    public interface IUserService
    {
        Task<GetUserDto> CreateUserAsync(GetUserDto dto, CancellationToken token);
        Task DeleteUserAsync(Guid userId, CancellationToken token);
        Task<GetUserDto> GetUserByIdAsync(Guid userId, CancellationToken token);
        Task<List<GetUserDto>> GetUsersAllAsync(CancellationToken token);
        Task UpdateUserAsync(GetUserDto dto, CancellationToken token);
    }
}
