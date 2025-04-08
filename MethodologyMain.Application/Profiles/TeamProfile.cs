using AutoMapper;
using MethodologyMain.Logic.Entities;
using MethodTeams.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamEntity, Team>();

            CreateMap<Team, TeamEntity>();
        }
    }
}
