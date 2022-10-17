using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.API.Logging
{
    [ExcludeFromCodeCoverage]
    public class TextFileLoggerProvider : ILoggerProvider
    {
        IConfiguration Configuration { get; }
        public TextFileLoggerProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new TextFileLogger(categoryName, Configuration);            
        }

        public void Dispose()
        {            
        }
    }
}
