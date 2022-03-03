namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class CpShopClose : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.ShopClose;
    }
}