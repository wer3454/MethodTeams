namespace MethodologyMain.Application.DTO
{
    public class UpdateTeamDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? HackathonId { get; set; }
    }
}
