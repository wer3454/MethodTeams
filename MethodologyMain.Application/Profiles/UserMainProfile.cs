using AutoMapper;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.Profiles
{
    public class UserMainProfile : Profile
    {
        public UserMainProfile()
        {
            CreateMap<UserMainEntity, UserMain>();
            CreateMap<UserMain, UserMainEntity>();
        }
    }
}
