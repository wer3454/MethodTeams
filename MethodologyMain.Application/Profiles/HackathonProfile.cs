using AutoMapper;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;

namespace MethodologyMain.Application.Profiles
{
    public class HackathonProfile : Profile
    {
        public HackathonProfile() 
        {
            CreateMap<HackathonEntity, Hackathon>();

            CreateMap<Hackathon, HackathonEntity>();

            CreateMap<List<HackathonEntity>, List<Hackathon>>();

            CreateMap<List<Hackathon>, List<HackathonEntity>>();
        }
    }
}
