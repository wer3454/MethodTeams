using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("hackathon", Schema = "mainSchema")]
    public class HackathonEntity
    {
        [Key]
        [Column("id")]
        public required Guid Id { get; set; }

        [Column("organizationId")]
        public required Guid OrganizationId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("prize")]
        public required decimal Prize { get; set; }

        [Column("minTeamSize")]
        public required int MinTeamSize { get; set; }

        [Column("maxTeamSize")]
        public required int MaxTeamSize { get; set; }

        [Column("startDate")]
        public required DateTime StartDate { get; set; }

        [Column("endDate")]
        public required DateTime EndDate { get; set; }

        [Column("additionalInfo")]
        public string AdditionalInfo { get; set; } = string.Empty;

        public OrganizationEntity Organization { get; set; } = null!;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();

        public List<HackathonTagEntity> Tags { get; set; } = new List<HackathonTagEntity>();

        public List<TrackEntity> Tracks { get; set; } = new List<TrackEntity>();
    }
}
