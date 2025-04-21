using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using MethodTeams.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Persistence.Repository
{
    public class UserRepository: GenericRepository<UserMainEntity>
    {
        public UserRepository(MyDbContext context) : base(context)
        {
        }


    }
}
