using AutoMapper;
using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using MethodTeams.Models;

namespace MethodologyMain.Application.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {

            CreateMap<TeamEntity, Team>()
                .ForMember(t => t.CreatedAt, conf => conf.MapFrom(t => t.TeamCreatedAt));

            CreateMap<Team, TeamEntity>()
                .ForMember(t => t.TeamCreatedAt, conf => conf.MapFrom(t => t.CreatedAt));
        }
    }
}
