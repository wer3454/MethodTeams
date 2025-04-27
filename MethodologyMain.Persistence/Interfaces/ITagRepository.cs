namespace MethodologyMain.Persistence.Interfaces
{
    public interface ITagRepository
    {
        Task AddTeamTags(Guid teamId, List<string> tagNames, CancellationToken token);
    }
}