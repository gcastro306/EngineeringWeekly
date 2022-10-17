using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.DTOS
{
    [ExcludeFromCodeCoverage]
    public class SwaggerInfo
    {
        /// <summary>
        /// Swagger root path
        /// </summary>
        public string? RootPath { get; set; }
        /// <summary>
        /// API Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// API Version
        /// </summary>
        public string? Version { get; set; }
    }
}
