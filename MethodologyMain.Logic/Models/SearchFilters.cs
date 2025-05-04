using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MethodologyMain.Logic.Models
{
    public class SearchFilters
    {
        public string? Query { get; set; } = string.Empty;
        public List<string>? Tags { get; set; } = null!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TabType? Tab { get; set; } = TabType.All;
    }
    public enum TabType
    {
        All,
        Upcoming,
        Past
    }
}
