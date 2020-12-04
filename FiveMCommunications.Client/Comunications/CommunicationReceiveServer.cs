namespace FiveMCommunications.Client.Comunications
{
    using System;
    using Contracts;
    using RPC;

    public class CommunicationReceiveServer : ICommunicationReceiveServer
	{
		private readonly CommunicationTarget _target;
		public CommunicationReceiveServer(CommunicationTarget target)
		{
			this._target = target;
		}

		public void On(Action<ICommunicationMessage> callback) => RpcManager.On(this._target.Event, callback);

		public void On<T>(Action<ICommunicationMessage, T> callback) => RpcManager.On(this._target.Event, callback);

		public void On<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => RpcManager.On(this._target.Event, callback);

		public void On<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => RpcManager.On(this._target.Event, callback);

		public void On<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => RpcManager.On(this._target.Event, callback);

		public void On<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => RpcManager.On(this._target.Event, callback);

		public void OnRequest(Action<ICommunicationMessage> callback) => RpcManager.OnRequest(this._target.Event, callback);

		public void OnRequest<T>(Action<ICommunicationMessage, T> callback) => RpcManager.OnRequest(this._target.Event, callback);

		public void OnRequest<T1, T2>(Action<ICommunicationMessage, T1, T2> callback) => RpcManager.OnRequest(this._target.Event, callback);

		public void OnRequest<T1, T2, T3>(Action<ICommunicationMessage, T1, T2, T3> callback) => RpcManager.OnRequest(this._target.Event, callback);

		public void OnRequest<T1, T2, T3, T4>(Action<ICommunicationMessage, T1, T2, T3, T4> callback) => RpcManager.OnRequest(this._target.Event, callback);

		public void OnRequest<T1, T2, T3, T4, T5>(Action<ICommunicationMessage, T1, T2, T3, T4, T5> callback) => RpcManager.OnRequest(this._target.Event, callback);
	}
}
