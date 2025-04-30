using MethodologyMain.Logic.Models;
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

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("startDate")]
        public required DateOnly StartDate { get; set; }

        [Column("endDate")]
        public required DateOnly EndDate { get; set; }

        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Column("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("minTeamSize")]
        public required int MinTeamSize { get; set; }

        [Column("maxTeamSize")]
        public required int MaxTeamSize { get; set; }

        [Column("website")]
        public string Website { get; set; } = string.Empty;

        [Column("prize")]
        public required string Prizes { get; set; }

        [Column("schedule")]
        public required string Schedule { get; set; }

        public OrganizationEntity Organization { get; set; } = null!;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();

        public List<HackathonTagEntity> Tags { get; set; } = new List<HackathonTagEntity>();
    }
}
