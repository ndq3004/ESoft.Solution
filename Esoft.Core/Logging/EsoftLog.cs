using Microsoft.Extensions.Logging;
namespace ESoft.Core.Logging
{
    public class CustomEsoftLog : ILogger
    {
        public CustomEsoftLog() { }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            //Custom the loggin message here
            string message = "";
            if (formatter != null)
            {
                message += formatter(state, exception);
            }
            // Print value
            Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {message}");
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

    }

    public class EsoftLog : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomEsoftLog();
        }

        public void Dispose()
        {
        }
    }

}
