namespace FiveMCommunications.Client.Rpc
{
    using System;
    using System.Collections.Generic;

    public class InboundMessage
    {
        public Guid Id { get; set; }

        public string Event { get; set; }

        public List<string> Payloads { get; set; }

        public static InboundMessage From(byte[] data) => RpcPacker.Unpack(data);

    }
}