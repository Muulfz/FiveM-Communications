namespace FiveMCommunications.Server.Communications
{
    using System;
    using Contacts;
    using Events;

    public class CommunicationReceiveServer : ICommunicationReceiveServer
	{
		private readonly string _event;
		private readonly EventManager _eventManager;

		public CommunicationReceiveServer(string @event, EventManager eventManager)
		{
			this._event = @event;
			this._eventManager = eventManager;
		}

		public void On(Action<ICommunicationMessage> callback) => this._eventManager.On(this._event, callback);

		public void On<T>(Action<ICommunicationMessage, T> callback) => this._eventManager.On(this._event, callback);

		public void On<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => this._eventManager.On(this._event, callback);

		public void On<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => this._eventManager.On(this._event, callback);

		public void On<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => this._eventManager.On(this._event, callback);

		public void On<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => this._eventManager.On(this._event, callback);

		public void OnRequest(Action<ICommunicationMessage> callback) => this._eventManager.OnRequest(this._event, callback);

		public void OnRequest<T>(Action<ICommunicationMessage, T> callback) => this._eventManager.OnRequest(this._event, callback);

		public void OnRequest<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => this._eventManager.OnRequest(this._event, callback);

		public void OnRequest<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => this._eventManager.OnRequest(this._event, callback);

		public void OnRequest<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => this._eventManager.OnRequest(this._event, callback);

		public void OnRequest<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => this._eventManager.OnRequest(this._event, callback);
	}
}
