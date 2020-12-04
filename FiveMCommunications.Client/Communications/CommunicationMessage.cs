namespace FiveMCommunications.Client.Communications
{
	using System;
	using Contracts;
	using Events;
	using Rpc;

	public class CommunicationMessage : ICommunicationMessage
	{
		private readonly EventManager _eventManager;

		private readonly bool _networked;

		public Guid Id { get; } = Guid.NewGuid();

		public string Event { get; }

		public CommunicationMessage(string @event)
		{
			this.Event = @event;
		}

		public CommunicationMessage(string @event, EventManager eventManager) : this(@event)
		{
			this._eventManager = eventManager;
		}

		public CommunicationMessage(string @event, Guid id, bool networked) : this(@event)
		{
			this.Id = id;
			this._networked = networked;
		}

		public void Reply(params object[] payloads)
		{
			if (this._networked)
			{
				RpcManager.Emit($"{this.Id}:{this.Event}", payloads);
			}
			else
			{
				this._eventManager.Emit($"{this.Id}:{this.Event}", payloads);
			}
		}
	}
}
