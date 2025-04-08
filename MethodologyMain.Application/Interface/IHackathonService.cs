using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.Interface
{
    public interface IHackathonService
    {
        Task<List<Hackathon>> GetHackathonsByFlexibleSearchAsync(HackathonFilterDto filterDto);
    }
}
