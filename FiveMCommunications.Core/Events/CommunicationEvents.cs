namespace FiveMCommunications.Core.Events
{
    /// <summary>
    /// Communication Core Events
    /// </summary>
    public class CommunicationEvents
    {
        /// <summary>
        /// The event that is fired when the client is relaying a log message to the server.
        /// </summary>
        public const string LogMirror = "fivemcommunication:log:mirror";
    }
}