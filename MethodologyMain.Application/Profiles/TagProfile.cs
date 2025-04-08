using AutoMapper;
using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagEntity, Tag>();
            CreateMap<Tag, TagEntity>();
        }
    }
}
