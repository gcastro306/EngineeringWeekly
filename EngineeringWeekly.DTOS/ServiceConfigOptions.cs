using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.DTOS
{
    [ExcludeFromCodeCoverage]
    public class ServiceConfigOptions
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the service version.
        /// </summary>
        /// <value>
        /// The service version.
        /// </value>
        public string ServiceVersion { get; set; }

        /// <summary>
        /// Gets or sets the hostin operative system
        /// </summary>
        /// <value>
        /// The operative system name.
        /// </value>
        public string OS { get; set; }

        /// <summary>
        /// Gets or sets the service environment.
        /// </summary>
        /// <value>
        /// The service environment.
        /// </value>
        public string Environment { get; set; }
        /// <summary>
        /// Pins mapped in the Raspberry Pi
        /// </summary>
    }
}
