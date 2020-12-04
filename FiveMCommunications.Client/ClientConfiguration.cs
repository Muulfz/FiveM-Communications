namespace FiveMCommunications.Client
{
    using Core.Diagnostics;
    public class ClientConfiguration
    {
        public static LogLevel MirrorLogLevel { get; set; } = LogLevel.Error;
        public static LogLevel ConsoleLogLevel { get; set; } = LogLevel.Error;
    }
}