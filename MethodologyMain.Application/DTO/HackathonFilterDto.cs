namespace MethodologyMain.Application.DTO
{
    public class HackathonFilterDto
    {
        public string? Name { get; set; }
        public Guid? OrganizationId { get; set; }
        public decimal? Prize { get; set; }

        public int? MinTeamSize { get; set; }

        public int? MaxTeamSize { get; set; }

        public DateOnly? StartDateFrom { get; set; }

        public DateOnly? StartDateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
