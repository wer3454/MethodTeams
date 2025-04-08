using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("hackthonTag")]
    public class HackathonTagEntity
    {
        [Column("hackathonId")]
        public Guid HackathonId { get; set; }

        [Column("tagId")]
        public Guid TagId { get; set; }

        public HackathonEntity Hackathon { get; set; } = null!;
        public TagEntity Tag { get; set; } = null!;
    }
}
