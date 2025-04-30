namespace MethodologyMain.Application.DTO
{
    public class CreateTeamDto
    {
        public Guid HackathonId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null!;
        public List<string> Members { get; set; } = null!;
        public required int MaxMembers { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
