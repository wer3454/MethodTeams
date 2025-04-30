using MethodologyMain.Logic.Entities;
using MethodTeams.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    // Модель данных для участника команды
    public class UserMain
    {
        public required Guid Id { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.MinValue;
        public string Education { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Location { get; set; } = string.Empty;
        public string Telegram { get; set; } = string.Empty;
        public string Github { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;

        public List<UserTeamEntity> Teams { get; set; } = new List<UserTeamEntity>();

        public List<UserTagEntity> Tags { get; set; } = new List<UserTagEntity>();
    }
}
