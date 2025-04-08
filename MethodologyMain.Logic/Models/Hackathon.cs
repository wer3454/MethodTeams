using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class Hackathon
    {
        public required Guid Id { get; set; }

        public required Guid OrganizationId { get; set; }

        public required string Name { get; set; }

        public required decimal Prize { get; set; }

        public required int MinTeamSize { get; set; }

        public required int MaxTeamSize { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

        public string AdditionalInfo { get; set; } = string.Empty;

        public OrganizationEntity Organization { get; set; } = null!;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();

        public List<HackathonTagEntity> Tags { get; set; } = new List<HackathonTagEntity>();

        public List<TrackEntity> Tracks { get; set; } = new List<TrackEntity>();
    }
}
