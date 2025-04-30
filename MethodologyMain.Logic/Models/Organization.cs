using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class Organization
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public List<HackathonEntity> Hackathons { get; set; } = new List<HackathonEntity>();
    }
}
