using MethodologyMain.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public string PrizesJson { get; set; }

        [Column("schedule")]
        public string ScheduleJson { get; set; }

        [NotMapped]
        public required List<Prize> Prizes
        {
            get => string.IsNullOrEmpty(PrizesJson)
                ? new List<Prize>()
                : JsonSerializer.Deserialize<List<Prize>>(PrizesJson);
            set => PrizesJson = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public required List<ScheduleItem> Schedule
        {
            get => string.IsNullOrEmpty(ScheduleJson)
                ? new List<ScheduleItem>()
                : JsonSerializer.Deserialize<List<ScheduleItem>>(ScheduleJson);
            set => ScheduleJson = JsonSerializer.Serialize(value);
        }

        public OrganizationEntity Organization { get; set; } = null!;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();

        public List<HackathonTagEntity> Tags { get; set; } = new List<HackathonTagEntity>();
    }
}
