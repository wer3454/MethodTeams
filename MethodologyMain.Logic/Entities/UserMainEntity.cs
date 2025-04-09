using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MethodologyMain.Logic.Entities
{
    [Table("teamMember", Schema ="mainSchema")]
    public class UserMainEntity
    {
        [Key]
        [Column("id")]
        public required Guid Id { get; set; }
        [Column("birthDate")]
        public DateTime BirthDate { get; set; }
        [Column("education")]
        public string Education { get; set; } = string.Empty;
        [Column("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [Column("lastName")]
        public string LastName { get; set; } = string.Empty;
        [Column("middleName")]
        public string Surname { get; set; } = string.Empty;
        [Column("userName")]
        public string UserName { get; set; } = string.Empty;
        [Column("Telegram")]
        public string Telegram { get; set; } = string.Empty;


        public List<UserTeamEntity> Teams { get; set; } = new List<UserTeamEntity>();

        public List<UserTagEntity> Tags { get; set; } = new List<UserTagEntity>();
    }
}
