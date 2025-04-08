using MethodologyMain.Application.DTO;
using MethodologyMain.Application.Interface;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using MethodologyMain.Persistence.Interfaces;
using System.Linq.Expressions;
using LinqKit;
using AutoMapper;

namespace MethodologyMain.Application.Services
{
    public class HackathonService : IHackathonService
    {
        private readonly IHackathonRepository hackathonRepository;
        private readonly IMapper mapper;
        public HackathonService(IHackathonRepository hackathonRepository, IMapper mapper)
        {
            this.hackathonRepository = hackathonRepository;
            this.mapper = mapper;
        }
        public async Task<List<Hackathon>> GetHackathonsByFlexibleSearchAsync(HackathonFilterDto filterDto)
        {
            Expression<Func<HackathonEntity, bool>> predicate = h => true;

            if (!string.IsNullOrEmpty(filterDto.Name))
                predicate = predicate.And(h => h.Name.ToLower().Contains(filterDto.Name.ToLower()));

            if (filterDto.OrganizationId.HasValue)
                predicate = predicate.And(h => h.OrganizationId == filterDto.OrganizationId);

            if(filterDto.StartDateFrom.HasValue)
                predicate = predicate.And(h => h.StartDate >= filterDto.StartDateFrom);

            if (filterDto.StartDateTo.HasValue)
                predicate = predicate.And(h => h.StartDate <= filterDto.StartDateTo);

            if (filterDto.StartDateTo.HasValue)
                predicate = predicate.And(h => h.MinTeamSize >= filterDto.MinTeamSize
                    && h.MaxTeamSize <= filterDto.MaxTeamSize);

            var hackEntities = await hackathonRepository.GetHackathonsByFlexibleSearchAsync(filterDto.Page,filterDto.PageSize ,predicate);

            return mapper.Map<List<Hackathon>>(hackEntities);
        }
    }
}
