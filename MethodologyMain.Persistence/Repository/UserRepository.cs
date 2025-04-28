using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;

namespace MethodologyMain.Persistence.Repository
{
    public class UserRepository: GenericRepository<UserMainEntity>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context)
        {
        }


    }
}
