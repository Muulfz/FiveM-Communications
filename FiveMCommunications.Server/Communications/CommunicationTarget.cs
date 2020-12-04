namespace FiveMCommunications.Server.Communications
{
    using Contacts;
    using Events;

    public class CommunicationTarget : ICommunicationTarget
	{
		private readonly EventManager _eventManager;

		public string Event { get; }

		public CommunicationTarget(EventManager eventManager, string @event)
		{
			this._eventManager = eventManager;
			this.Event = @event;
		}

		public ICommunicationTransmitClient ToClient(IClient client) => new CommunicationTransmitClient(this.Event, client);

		public ICommunicationReceiveClient FromClient(IClient client) => new CommunicationReceiveClient(this.Event, client);

		public ICommunicationTransmitClient ToClients() => new CommunicationTransmitClient(this.Event);

		public ICommunicationReceiveClient FromClients() => new CommunicationReceiveClient(this.Event);

		public ICommunicationTransmitServer ToServer() => new CommunicationTransmitServer(this.Event, this._eventManager);

		public ICommunicationReceiveServer FromServer() => new CommunicationReceiveServer(this.Event, this._eventManager);
	}
}
