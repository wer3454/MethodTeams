using MethodologyMain.Logic.Models;

namespace MethodologyMain.Application.DTO
{
    public class GetHackathonDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string OrganizerLogo { get; set; } = string.Empty;
        public TeamSize TeamSize { get; set; } = null!;
        public string Website { get; set; } = string.Empty;
        public List<Prize> Prizes { get; set; } = null!;
        public List<ScheduleItem> Schedule { get; set; } = null!;

    }
}
