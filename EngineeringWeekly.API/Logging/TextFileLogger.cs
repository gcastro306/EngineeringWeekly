using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace EngineeringWeekly.API.Logging
{
    [ExcludeFromCodeCoverage]
    public class TextFileLogger : LogBase
    {       
        public TextFileLogger(string categoryName, IConfiguration configuration) : base(categoryName, configuration, "TextFile") {}
        public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel, eventId))
            {
                object log;
                if (logLevel != LogLevel.Error && logLevel != LogLevel.Critical)
                {
                    log = new SystemLog
                    {
                        SystemLogStatusId = (int)logLevel,
                        LogDetail = formatter(state, exception),
                        ReferenceId = !string.IsNullOrEmpty(eventId.Name) ? (int?)eventId.Id : null
                    };

                }
                else
                {
                    if (exception != null)
                    {
                        StackTrace trace = new StackTrace(exception, true);
                        log = new SystemErrorLog
                        {
                            SystemLogStatusId = (int)logLevel,
                            LogDetail = exception.Message,
                            ReferenceId = !string.IsNullOrEmpty(eventId.Name) ? (int?)eventId.Id : null,
                            Class = trace.GetFrame(0).GetMethod().ReflectedType.FullName,
                            Line = trace.GetFrame(0).GetFileLineNumber(),
                            StackTrace = exception.StackTrace
                        };
                    }
                    else
                    {
                        log = new SystemErrorLog
                        {
                            SystemLogStatusId = (int)logLevel,
                            LogDetail = formatter(state, exception),
                            ReferenceId = !string.IsNullOrEmpty(eventId.Name) ? (int?)eventId.Id : null,
                            Class = "Unidentified class",
                            Line = 0,
                            StackTrace = formatter(state, exception)
                        };
                    }
                }
                string textLogPath = !string.IsNullOrWhiteSpace(LoggerConfig.TextLogPath) ? $"{LoggerConfig.TextLogPath}\\{logLevel}" : $"{Directory.GetCurrentDirectory()}\\Logs\\{logLevel}";
                if (!Directory.Exists(textLogPath))
                    Directory.CreateDirectory(textLogPath);

                var logBuilder = new StringBuilder();
                    textLogPath = $"{textLogPath}\\{DateTime.UtcNow.ToString("yyyyMMdd")}-Log.txt";
                    logBuilder.AppendLine($"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff zzz")} {CategoryName}");
                    logBuilder.AppendLine(log.ToString());
                    logBuilder.AppendLine(new string('*', 150));

                using (FileStream fs = new FileStream(textLogPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(logBuilder.ToString());
                    }
                }
            }
        }
    }
}
