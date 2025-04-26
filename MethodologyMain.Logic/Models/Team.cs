using MethodologyMain.Logic.Entities;

namespace MethodTeams.Models
{

    public class Team
    {
        public required Guid Id { get; set; }
        public required Guid HackathonId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int MaxMembers { get; set; }
        public required Guid CaptainId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационное свойство для связи с участниками
        public List<TeamTagEntity> Tags { get; set; } = new List<TeamTagEntity>();
        public List<UserTeamEntity> Members { get; set; } = new List<UserTeamEntity>();
        public HackathonEntity Hackathon { get; set; } = null!;
    }

    
}
