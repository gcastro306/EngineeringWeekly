using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.API.Logging
{
    [ExcludeFromCodeCoverage]
    public class SystemErrorLog
    {
        public long Id { get; set; } = 0;
        public int SystemLogStatusId { get; set; }
        public long? ReferenceId { get; set; } = null;
        public string? LogDetail { get; set; }
        public string? Class { get; set; }
        public int Line { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerException { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
