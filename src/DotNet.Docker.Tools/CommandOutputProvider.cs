using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace DotNet.Tools.Docker
{
    public class CommandOutputProvider : ILoggerProvider
    {
        private readonly bool _isWindows;

        public CommandOutputProvider()
        {
            _isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public ILogger CreateLogger(string name)
        {
            return new CommandOutputLogger(this, name, useConsoleColor: _isWindows);
        }

        public void Dispose()
        {
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Information;
    }
}