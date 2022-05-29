namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpAddRecipe : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.AddRecipe;
        public int Recipe { get; set; }
    }
}