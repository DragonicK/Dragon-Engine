namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class CpStopCraft : IMessagePacket { 
        public MessageHeader Header { get; set; } = MessageHeader.StopCraft;
    }
}