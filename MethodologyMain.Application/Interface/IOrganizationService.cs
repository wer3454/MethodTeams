using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Models;

namespace MethodologyMain.Application.Interface
{
    public interface IOrganizationService
    {
        Task<GetOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto org, CancellationToken token);
        Task<GetOrganizationDto> GetOrganizationByIdAsync(Guid orgId, CancellationToken token);
        Task<List<GetOrganizationDto>> GetOrganizationsAllAsync(CancellationToken token);
    }
}