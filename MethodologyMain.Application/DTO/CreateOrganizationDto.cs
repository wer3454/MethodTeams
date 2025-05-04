using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.DTO
{
    public class CreateOrganizationDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
    }
}
