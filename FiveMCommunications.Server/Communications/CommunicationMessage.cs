namespace FiveMCommunications.Server.Communications
{
    using System;
    using Contacts;
    using Events;
    using Rpc;

	public class CommunicationMessage : ICommunicationMessage
	{
		private readonly EventManager _eventManager;

		public Guid Id { get; } = Guid.NewGuid();

		public string Event { get; }

		public IClient Client { get; }

		public CommunicationMessage(string @event)
		{
			this.Event = @event;
		}

		public CommunicationMessage(string @event, EventManager eventManager) : this(@event)
		{
			this._eventManager = eventManager;
		}

		public CommunicationMessage(string @event, IClient client) : this(@event)
		{
			this.Client = client;
		}

		public CommunicationMessage(string @event, Guid id, IClient client) : this(@event, client)
		{
			this.Id = id;
		}

		public void Reply(params object[] payloads)
		{
			if (this.Client == null)
			{
				this._eventManager.Emit($"{this.Id}:{this.Event}", payloads);
			}
			else
			{
				RpcManager.Emit($"{this.Id}:{this.Event}", this.Client, payloads);
			}
		}
	}
}
