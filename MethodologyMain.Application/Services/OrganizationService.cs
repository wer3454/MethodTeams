using AutoMapper;
using MethodologyMain.Application.DTO;
using MethodologyMain.Application.Interface;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using MethodologyMain.Persistence.Interfaces;

namespace MethodologyMain.Application.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository orgRepo;
        private readonly IMapper mapper;

        public OrganizationService(IOrganizationRepository orgRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.orgRepo = orgRepo;
        }

        public async Task<GetOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto org, CancellationToken token)
        {
            var orgEntity = new OrganizationEntity
            {
                Id = Guid.NewGuid(),
                Name = org.Name,
                Description = org.Description,
                Logo = org.Logo
            };
            await orgRepo.AddAsync(orgEntity, token);
            return mapper.Map<GetOrganizationDto>(orgEntity);
        }
        public async Task<GetOrganizationDto> GetOrganizationByIdAsync(Guid orgId, CancellationToken token)
        {
            // await validation.CheckTeamExistsAsync(teamId, token);
            var org = await orgRepo.GetByIdAsync(orgId, token);
            return mapper.Map<GetOrganizationDto>(org);
        }

        public async Task<List<GetOrganizationDto>> GetOrganizationsAllAsync(CancellationToken token)
        {
            var orgs = await orgRepo.GetAllAsync(token);
            return mapper.Map<List<GetOrganizationDto>>(orgs);
        }
    }
}
