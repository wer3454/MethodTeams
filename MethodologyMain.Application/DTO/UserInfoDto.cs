using MethodologyMain.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.DTO
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public string? Education { get; set; }
        public List<UserTagEntity>? Tags { get; set; }
    }
}
