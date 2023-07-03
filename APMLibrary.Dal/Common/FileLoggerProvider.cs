using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Common
{
    internal partial class FileLoggerProvider : ILoggerProvider
    {
        protected readonly IDisposable? _onChangeToken = default!;
        protected FileLoggerOptions _options = default!;
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options) : base()
        {
            this._options = options.CurrentValue;
            this._onChangeToken = options.OnChange(config => this._options = config);
        }
        protected FileLoggerOptions GetLoggerConfiguration() => this._options;

        public class FileLogger : ILogger, IDisposable
        {
            protected readonly Func<FileLoggerOptions> _loggerOptions = default!;
            protected readonly string _categoryName = default!;

            public FileLogger(string category, Func<FileLoggerOptions> options) : base()
                => (this._loggerOptions, this._categoryName) = (options, category);

            public virtual IDisposable? BeginScope<TState>(TState state) where TState : notnull => this;
            public virtual bool IsEnabled(LogLevel logLevel) => true;

            public virtual void Dispose() { }

            public virtual async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, 
                Func<TState, Exception?, string> formatter)
            {
                var timestampValue = string.Format("\n[DateTime]: [{0}]", DateTime.UtcNow);

                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._loggerOptions().FilePath);
                using (var fileWriter = new StreamWriter(new FileStream(filepath, FileMode.Append)))
                {
                    await fileWriter.WriteLineAsync(timestampValue);
                    await fileWriter.WriteLineAsync($"{formatter(state, exception)}\n");
                }
            }
        }
        public virtual ILogger CreateLogger(string category) => new FileLogger(category, this.GetLoggerConfiguration);
        public virtual void Dispose() { this._onChangeToken?.Dispose(); }
    }
}
