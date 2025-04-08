using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("userTeam",Schema ="mainSchema")]
    public class UserTeamEntity
    {

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("teamId")]
        public Guid TeamId { get; set; }

        public UserMainEntity User { get; set; } = null!;

        public TeamEntity Team { get; set; } = null!;
    }
}
