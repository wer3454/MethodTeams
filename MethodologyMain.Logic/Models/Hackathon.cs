using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class Hackathon
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required Guid OrganizationId { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public TeamSize TeamSize { get; set; } = null!;
        public int MinMembers { get; set; } = 2;
        public int MaxMembers { get; set; } = 8;
        public List<Prize> Prizes { get; set; } = null!;
        public List<ScheduleItem> Schedule { get; set; } = null!;
        public OrganizationEntity Organization { get; set; } = null!;
        public List<HackathonTagEntity> Tags { get; set; } = new List<HackathonTagEntity>();
        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
    }
}
