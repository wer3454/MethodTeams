namespace MethodologyMain.Application.DTO
{

    // DTO для информации о команде
    public class TeamInfoDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid CaptainId { get; set; }
        public Guid HackathonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MemberCount { get; set; }
    }
}
