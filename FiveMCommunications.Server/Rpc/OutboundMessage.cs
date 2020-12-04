namespace FiveMCommunications.Server.Rpc
{
    using System;
    using System.Collections.Generic;
    using Communications.Contacts;
    using Newtonsoft.Json;

    public class OutboundMessage
	{
		public Guid Id { get; set; }

		[JsonIgnore]
		public IClient Target { get; set; }

		public string Event { get; set; }

		public List<string> Payloads { get; set; } = new List<string>();

		public byte[] Pack() => RpcPacker.Pack(this);
	}
}
