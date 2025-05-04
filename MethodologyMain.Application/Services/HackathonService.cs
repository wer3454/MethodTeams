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
        private readonly IHackathonRepository hackRepo;
        private readonly ITagRepository tagRepo;
        private readonly IMapper mapper;
        public HackathonService(
            IHackathonRepository hackRepo,
            ITagRepository tagRepo,
            IMapper mapper)
        {
            this.hackRepo = hackRepo;
            this.tagRepo = tagRepo;
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

            var hackEntities = await hackRepo.GetHackathonsByFlexibleSearchAsync(filterDto.Page,filterDto.PageSize ,predicate);

            return mapper.Map<List<Hackathon>>(hackEntities);
        }

        public async Task<List<GetHackathonDto>> GetHackWithFilterAsync(SearchFilters filters, CancellationToken token)
        {
            var hacks = await hackRepo.GetHackathonsWithSearchAsync(filters, token);
            return mapper.Map<List<GetHackathonDto>>(hacks);
        }
        public async Task<GetHackathonDto> CreateHackAsync(CreateHackathonDto dto, CancellationToken token)
        {
            var hack = new HackathonEntity
            {
                Id = Guid.NewGuid(),
                OrganizationId = dto.OrganizationId,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ImageUrl = dto.ImageUrl,
                MaxTeamSize = dto.TeamSize.Max,
                MinTeamSize = dto.TeamSize.Min,
                Prizes = dto.Prizes,
                Schedule = dto.Schedule,
                Location = dto.Location,
                Website = dto.Website,
            };
            await hackRepo.AddAsync(hack, token);
            await tagRepo.AddHackTags(hack.Id, dto.Tags, token);
            return mapper.Map<GetHackathonDto>(hack);
        }

        // Получение информации о хакатоне по ID
        public async Task<GetHackathonDto> GetHackByIdAsync(Guid hackId, CancellationToken token)
        {
            // await validation.CheckTeamExistsAsync(teamId, token);
            var hack = await hackRepo.GetByIdAsync(hackId, token);
            return mapper.Map<GetHackathonDto>(hack);
        }

        // Получение списка хакатонов
        public async Task<List<GetHackathonDto>> GetHacksAllAsync(CancellationToken token)
        {
            var hacks = await hackRepo.GetAllHackathonsAsync(token);
            return mapper.Map<List<GetHackathonDto>>(hacks);
        }
    }
}
