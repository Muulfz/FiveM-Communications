namespace FiveMCommunications.Server.Utils
{
	public interface IConnectionDeferrals
	{
		string Message { set; }

		void Defer();

		void ShowCard(string json);

		void Allow();

		void Reject(string message);
	}
}
