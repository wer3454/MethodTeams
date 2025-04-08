using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("tag",Schema ="mainSchema")]
    public class TagEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("tagName")]
        public required string TagName { get; set; }

        [Column("tagClassName")]
        public required string TagClassName { get; set; }

        public List<UserTagEntity> Users { get; set; } = new List<UserTagEntity>();

        public List<HackathonTagEntity> Hacksthons { get; set; } = new List<HackathonTagEntity>();
    }
}
