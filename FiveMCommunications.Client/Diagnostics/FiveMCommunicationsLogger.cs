namespace FiveMCommunications.Client.Diagnostics
{
    using System;
    using System.Linq;
    using Core.Diagnostics;
    using Core.Events;
    using Rpc;

    public class FiveMCommunicationsLogger : ILogger
    {
		public string Prefix { get; }

        public FiveMCommunicationsLogger(string prefix = "")
        {
            this.Prefix = prefix;
        }

        public void Trace(string message)
        {
            Log(message, LogLevel.Trace);
        }

        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Warn(string message)
        {
            Log(message, LogLevel.Warn);
        }

        public void Error(Exception exception)
        {
            Error(exception, "ERROR");
        }

        public void Error(Exception exception, string message)
        {
            Log($"{message}: {exception.Message}", LogLevel.Error);
        }

        public void Log(string message, LogLevel level)
        {
            if (ClientConfiguration.ConsoleLogLevel > level && ClientConfiguration.MirrorLogLevel > level) return;

            var output = $"{DateTime.Now:s} [{level}]";

            if (!string.IsNullOrEmpty(this.Prefix)) output += $" [{this.Prefix}]";

            var lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var formattedMessage = string.Join(Environment.NewLine, lines.Select(l => $"{output} {l}"));

            if (ClientConfiguration.ConsoleLogLevel <= level)
            {
                CitizenFX.Core.Debug.Write($"{formattedMessage}{Environment.NewLine}");
            }

            if (ClientConfiguration.MirrorLogLevel <= level && !message.Contains(CommunicationEvents.LogMirror))
            {
                RpcManager.Emit(CommunicationEvents.LogMirror, DateTime.UtcNow, level, this.Prefix, message);
            }
        }
	}
}