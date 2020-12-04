namespace FiveMCommunications.Server.Communications.Contacts
{
	public interface ICommunicationManager
	{
		ICommunicationTarget Event(string @event);
	}
}
