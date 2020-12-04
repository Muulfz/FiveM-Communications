namespace FiveMCommunications.Server.Events
{
    public class FivemServerEvents
    {
        public const string HostingSession = "hostingSession";

        public const string HostedSession = "HostedSession";

        public const string PlayerConnecting = "playerConnecting";

        public const string PlayerDropped = "playerDropped";

        public const string ResourceStart = "onResourceStart";

        public const string ResourceStop = "onResourceStop";

        public const string RconCommand = "rconCommand";

        public const string Explosion = "explosionEvent";

        public const string ResourceListRefresh = "onResourceListRefresh";

        public const string WeaponDamages = "weaponDamageEvent";

        public const string VehicleComponentControl = "vehicleComponentControlEvent";

        public const string RespawnPlayerPed = "respawnPlayerPedEvent";

        public const string EntityCreated = "entityCreated";

        public const string EntityCreating = "entityCreating";

        public const string PlayerEnteredScope = "playerEnteredScope";

        public const string PlayerLeftScope = "playerLeftScope";
    }
}