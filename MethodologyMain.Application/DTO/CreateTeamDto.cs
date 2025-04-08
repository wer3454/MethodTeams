using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.DTO
{
    public class CreateTeamDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid EventId { get; set; }
    }
}
