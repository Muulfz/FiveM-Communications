namespace FiveMCommunications.Server.Communications
{
    using System;
    using System.Threading.Tasks;
    using Contacts;
    using Events;

    public class CommunicationTransmitServer : ICommunicationTransmitServer
	{
		private readonly string _event;
		private readonly EventManager _eventManager;

		public CommunicationTransmitServer(string @event, EventManager eventManager)
		{
			this._event = @event;
			this._eventManager = eventManager;
		}
 
		public void Emit(params object[] payloads)
		{
			this._eventManager.Emit(this._event, payloads);
		}

		public async Task<T> Request<T>(params object[] payloads) => await this._eventManager.Request<T>(this._event, payloads);

		public async Task<Tuple<T1, T2>> Request<T1, T2>(params object[] payloads) => await this._eventManager.Request<Tuple<T1, T2>>(this._event, payloads);

		public async Task<Tuple<T1, T2, T3>> Request<T1, T2, T3>(params object[] payloads) => await this._eventManager.Request<Tuple<T1, T2, T3>>(this._event, payloads);

		public async Task<Tuple<T1, T2, T3, T4>> Request<T1, T2, T3, T4>(params object[] payloads) => await this._eventManager.Request<Tuple<T1, T2, T3, T4>>(this._event, payloads);

		public async Task<Tuple<T1, T2, T3, T4, T5>> Request<T1, T2, T3, T4, T5>(params object[] payloads) => await this._eventManager.Request<Tuple<T1, T2, T3, T4, T5>>(this._event, payloads);
	}
}
