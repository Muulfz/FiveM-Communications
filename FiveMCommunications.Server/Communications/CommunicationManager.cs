namespace FiveMCommunications.Server.Communications
{
    using Contacts;
    using Events;

    public class CommunicationManager : ICommunicationManager
    {
        private readonly EventManager _eventManager;

        public CommunicationManager(EventManager eventManager)
        {
            this._eventManager = eventManager;
        }

        public ICommunicationTarget Event(string @event) => new CommunicationTarget(this._eventManager, @event);
    }
}