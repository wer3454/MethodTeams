namespace MethodTeams.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CaptainId { get; set; }
        public int EventId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационное свойство для связи с участниками
        public virtual ICollection<TeamMember> Members { get; set; }
    }

    // Модель данных для участника команды
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }

        // Навигационное свойство для связи с командой
        public virtual Team Team { get; set; }
    }
}
