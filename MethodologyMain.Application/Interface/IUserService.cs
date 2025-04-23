using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;

namespace MethodologyMain.Application.Interface
{
    public interface IUserService
    {
        Task<UserMainEntity> CreateUserAsync(
            string UserName,
            string description,
            Guid captainId,
            DateTime BirthDate,
            CancellationToken token
            );

    }
}
