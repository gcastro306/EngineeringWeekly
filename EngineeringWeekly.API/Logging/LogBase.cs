using EngineeringWeekly.DTOS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.API.Logging
{
    [ExcludeFromCodeCoverage]
    public abstract class LogBase : ILogger
    {
        string LogProvider { get; }
        internal string CategoryName { get; }
        internal IConfiguration Configuration { get; }
        internal LoggerConfig LoggerConfig { get; }
        public LogBase(string categoryName, IConfiguration configuration, string logProvider)
        {
            CategoryName = categoryName;
            Configuration = configuration;
            LogProvider = logProvider;
            LoggerConfig = new LoggerConfig()
            {
                TextLogPath = configuration.GetSection("LoggerConfig").GetValue<string>("TextLogPath")
            };
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public virtual bool IsEnabled(LogLevel logLevel, EventId eventId)
        {
            bool returnValue = true;
            if (logLevel != LogLevel.Error)
            {
                List<string> conf = Configuration.GetSection($"Logging:{LogProvider}:LogLevels").Get<List<string>>();
                bool allowAllEvents = Configuration.GetSection($"Logging:{LogProvider}:AllowAllEvents").Get<bool>();
                if (conf != null)
                    returnValue = !string.IsNullOrEmpty(eventId.Name) && conf.Contains(logLevel.ToString());
                else
                    returnValue = allowAllEvents ? true : !string.IsNullOrEmpty(eventId.Name);
            }
            return returnValue;
        }

        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}
