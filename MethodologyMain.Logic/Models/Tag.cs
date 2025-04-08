using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public required string TagName { get; set; }
        public required string TagClassName { get; set; }

        public List<UserTagEntity> Users { get; set; } = new List<UserTagEntity>();

        public List<HackathonTagEntity> Hacksthons { get; set; } = new List<HackathonTagEntity>();
    }
}
