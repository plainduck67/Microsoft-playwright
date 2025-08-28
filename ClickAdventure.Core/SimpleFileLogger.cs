using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ClickAdventure.Core
{
    public class SimpleFileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly string _categoryName;
        private static readonly object _lock = new object();

        public SimpleFileLogger(string categoryName, string logFilePath)
        {
            _categoryName = categoryName;
            _logFilePath = logFilePath;
        }

        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var message = $"{DateTime.UtcNow:o} [{logLevel}] {_categoryName}: {formatter(state, exception)}";
            lock (_lock)
            {
                File.AppendAllText(_logFilePath, message + Environment.NewLine);
            }
        }
    }

    public class SimpleFileLoggerProvider : ILoggerProvider
    {
        private readonly string _logFilePath;
        public SimpleFileLoggerProvider(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        public ILogger CreateLogger(string categoryName) => new SimpleFileLogger(categoryName, _logFilePath);
        public void Dispose() { }
    }
}
