using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("teamTag", Schema = "mainSchema")]
    public class TeamTagEntity
    {
        [Column("teamId")]
        public Guid TeamId { get; set; }

        [Column("tagId")]
        public Guid TagId { get; set; }

        public TeamEntity Team { get; set; } = null!;

        public TagEntity Tag { get; set; } = null!;
    }
}
