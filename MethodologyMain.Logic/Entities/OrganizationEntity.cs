using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("organization", Schema = "mainSchema")]
    public class OrganizationEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("description")]
        public required string Description { get; set; }

        [Column("linkToWebsite")]
        public required string LinkToWebSite { get; set; } = string.Empty;

        public List<HackathonEntity> Hackathons { get; set; } = new List<HackathonEntity>();
    }
}
