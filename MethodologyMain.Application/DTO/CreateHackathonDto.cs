using MethodologyMain.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.DTO
{
    public class CreateHackathonDto
    {
        public required Guid OrganizationId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public TeamSize TeamSize { get; set; } = null!;
        public string Website { get; set; } = string.Empty;
        public List<Prize> Prizes { get; set; } = null!;
        public List<ScheduleItem> Schedule { get; set; } = null!;
    }
}
