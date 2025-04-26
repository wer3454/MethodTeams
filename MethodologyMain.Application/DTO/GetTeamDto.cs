using MethodologyMain.Logic.Entities;

namespace MethodologyMain.Application.DTO
{
    class GetTeamDto
    {
        public Guid Id { get; set; }
        public Guid HackathonId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null!;
        public List<string> Members { get; set; } = null!;
        public int MaxMembers { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
