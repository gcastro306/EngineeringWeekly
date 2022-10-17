using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.API.Logging
{
    [ExcludeFromCodeCoverage]
    public class SystemLog  
    {
        public long Id { get; set; } = 0;
        public int SystemLogStatusId { get; set; }
        public long? ReferenceId { get; set; } = null;
        public string? LogDetail { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}