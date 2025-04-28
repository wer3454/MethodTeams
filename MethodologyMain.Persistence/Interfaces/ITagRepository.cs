namespace MethodologyMain.Persistence.Interfaces
{
    public interface ITagRepository
    {
        Task AddTeamTags(Guid teamId, List<string> tagNames, CancellationToken token);
        Task AddUserTags(Guid userId, List<string> tagNames, CancellationToken token);
        Task AddHackTags(Guid hackId, List<string> tagNames, CancellationToken token);
    }
}