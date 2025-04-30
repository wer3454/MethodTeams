using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Models;

namespace MethodologyMain.Application.Interface
{
    public interface IHackathonService
    {
        Task<List<Hackathon>> GetHackathonsByFlexibleSearchAsync(HackathonFilterDto filterDto);
        Task<GetHackathonDto> GetHackByIdAsync(Guid hackId, CancellationToken token);
        Task<List<GetHackathonDto>> GetHacksAllAsync(CancellationToken token);
    }
}
