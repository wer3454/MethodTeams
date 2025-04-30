using MethodologyMain.Logic.Entities;
using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;

namespace MethodologyMain.Persistence.Repository
{
    public class OrganizationRepository : GenericRepository<OrganizationEntity>, IOrganizationRepository
    {
        public OrganizationRepository(MyDbContext context) : base(context)
        {
        }
    }
}
