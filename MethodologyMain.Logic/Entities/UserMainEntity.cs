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
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        [Column("education")]
        public string Education { get; set; } = string.Empty;

        [Column("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("middleName")]
        public string MiddleName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("userName")]
        public string UserName { get; set; } = string.Empty;

        [Column("photoUrl")]
        public string PhotoUrl { get; set; } = string.Empty;

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Column("telegram")]
        public string Telegram { get; set; } = string.Empty;

        [Column("github")]
        public string Github { get; set; } = string.Empty;

        [Column("website")]
        public string Website { get; set; } = string.Empty;

        [Column("skills")]
        public string Skills { get; set; } = string.Empty;


        public List<UserTeamEntity> Teams { get; set; } = new List<UserTeamEntity>();

        public List<UserTagEntity> Tags { get; set; } = new List<UserTagEntity>();
    }
}
