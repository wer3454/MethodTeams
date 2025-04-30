using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserMainEntity>
    {
        Task<bool> CheckUserTeamInHackAsync(Guid userId, Guid hackathonId, CancellationToken token);
        Task UpdateAsync(UserMainEntity user, CancellationToken token);
        Task RemoveAsync(Guid userId, CancellationToken token);
        Task<UserMainEntity> GetUserByIdAsync(Guid userId, CancellationToken token);
        Task<List<UserMainEntity>> GetUsersAllAsync(CancellationToken token);

    }
}
