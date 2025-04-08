using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Entities
{
    [Table("userTag",Schema ="mainSchema")]
    public class UserTagEntity
    {
        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("tagId")]
        public Guid TagId { get; set; }

        public UserMainEntity User { get; set; } = null!;

        public TagEntity Tag { get; set; } = null!;
    }
}
