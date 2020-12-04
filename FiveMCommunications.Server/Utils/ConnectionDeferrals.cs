namespace FiveMCommunications.Server.Utils
{
    public class ConnectionDeferrals : IConnectionDeferrals
	{
		private readonly dynamic _deferrals;

		public string Message
		{
			set => this._deferrals.update(value);
		}

		public ConnectionDeferrals(dynamic deferrals)
		{
			this._deferrals = deferrals;
		}

		public void Defer()
		{
			this._deferrals.defer();
		}

		public void ShowCard(string json)
		{
			this._deferrals.presentCard(json);
		}

		public void Allow()
		{
			this._deferrals.done();
		}

		public void Reject(string message)
		{
			this._deferrals.done(message);
		}
	}
}
