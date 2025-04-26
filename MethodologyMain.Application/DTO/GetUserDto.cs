

namespace MethodologyMain.Application.DTO
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null!;
        public string PhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Github { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = null!;
    }
}
