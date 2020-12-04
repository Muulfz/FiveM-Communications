namespace FiveMCommunications.Client.Communications.Contracts
{
	public interface ICommunicationMessage
	{
		/// <summary>
		/// Replies to the event with the specified payloads.
		/// </summary>
		/// <param name="payloads">The payloads to reply to the event with.</param>
		void Reply(params object[] payloads);
	}
}
