using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class Track
    {
        public Guid Id { get; set; }

        public Guid HackathonId { get; set; }

        public string TrackName { get; set; } = string.Empty;

        public string TrackAdditionalInfo { get; set; } = string.Empty;

        public HackathonEntity Hackathon { get; set; } = null!;
    }
}
