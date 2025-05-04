using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class ScheduleItem
    {
        public required DateOnly Date { get; set; }
        public required string Time { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    
}
