namespace FiveMCommunications.Server.Communications
{
    using System;
    using Contacts;
    using Rpc;

    public class CommunicationReceiveClient : ICommunicationReceiveClient
	{
		private readonly string _event;
		private readonly IClient _target;

		public CommunicationReceiveClient(string @event)
		{
			this._event = @event;
		}

		public CommunicationReceiveClient(string @event, IClient client) : this(@event)
		{
			this._target = client;
		}

		public void On(Action<ICommunicationMessage> callback) => RpcManager.On(this._event, this._target, callback);

		public void On<T>(Action<ICommunicationMessage, T> callback) => RpcManager.On(this._event, this._target, callback);

		public void On<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => RpcManager.On(this._event, this._target, callback);

		public void On<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => RpcManager.On(this._event, this._target, callback);

		public void On<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => RpcManager.On(this._event, this._target, callback);

		public void On<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => RpcManager.On(this._event, this._target, callback);

		public void OnRequest(Action<ICommunicationMessage> callback) => RpcManager.OnRequest(this._event, this._target, callback);

		public void OnRequest<T>(Action<ICommunicationMessage, T> callback) => RpcManager.OnRequest(this._event, this._target, callback);

		public void OnRequest<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => RpcManager.OnRequest(this._event, this._target, callback);

		public void OnRequest<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => RpcManager.OnRequest(this._event, this._target, callback);

		public void OnRequest<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => RpcManager.OnRequest(this._event, this._target, callback);

		public void OnRequest<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => RpcManager.OnRequest(this._event, this._target, callback);
	}
}
