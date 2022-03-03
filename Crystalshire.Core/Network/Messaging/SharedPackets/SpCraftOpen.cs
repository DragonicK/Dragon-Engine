namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpCraftOpen : IMessagePacket {    
        public MessageHeader Header { get; set; } = MessageHeader.CraftOpen;
    }
}