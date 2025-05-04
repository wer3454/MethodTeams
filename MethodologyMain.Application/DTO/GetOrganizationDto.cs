namespace MethodologyMain.Application.DTO
{
    public class GetOrganizationDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
    }
}
