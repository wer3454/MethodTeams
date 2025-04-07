namespace MethodTeams.DTO
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
    }

    // DTO для добавления пользователя
    public class AddUserDto
    {
        public int UserId { get; set; }
    }

    // DTO для информации о команде
    public class TeamInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CaptainId { get; set; }
        public int EventId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MemberCount { get; set; }
    }
}
