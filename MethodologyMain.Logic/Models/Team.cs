using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MethodTeams.Models
{

    public class Team
    {
        public required Guid Id { get; set; }

        public required Guid HackathonId { get; set; }

        public required string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public required Guid CaptainId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Навигационное свойство для связи с участниками
        public List<UserTeamEntity> Members { get; set; } = new List<UserTeamEntity>();
        public HackathonEntity Hackathon { get; set; } = null!;
    }

    
}
