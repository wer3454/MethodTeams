using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("track",Schema = "mainSchema")]
    public class TrackEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("hackathonId")]
        public Guid HackathonId { get; set; }

        [Column("trackName")]
        public string TrackName { get; set; } = string.Empty;

        [Column("trackAdditionalInfo")]
        public string TrackAdditionalInfo { get; set; } = string.Empty;

        public HackathonEntity Hackathon { get; set; } = null!;
    }
}
