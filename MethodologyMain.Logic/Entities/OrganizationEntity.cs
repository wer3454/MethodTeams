using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Description { get; set; } = string.Empty;

        [Column("logo")]
        public string Logo { get; set; } = string.Empty;

        public List<HackathonEntity> Hackathons { get; set; } = new List<HackathonEntity>();
    }
}
