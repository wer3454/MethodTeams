using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MethodologyMain.Logic.Entities
{
    [Table("team", Schema ="mainSchema")]
    public class TeamEntity
    {
        [Key]
        [Column("id")]
        public required Guid Id { get; set; }

        [Column("hackathonId")]
        public required Guid HackathonId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("captainId")]
        public required Guid CaptainId { get; set; }

        [Column("teamCreatedAt")]
        public DateTime TeamCreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационное свойство для связи с участниками
        public List<TeamTagEntity> Tags { get; set; } = new List<TeamTagEntity>();
        public List<UserTeamEntity> Members { get; set; } = new List<UserTeamEntity>();
        public HackathonEntity Hackathon { get; set; } = null!;
    }
}
