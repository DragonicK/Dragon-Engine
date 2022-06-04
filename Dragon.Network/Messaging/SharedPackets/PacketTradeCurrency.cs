namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketTradeCurrency : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.TradeCurrency;
    public int StarterAmount { get; set; }
    public int InvitedAmount { get; set; }
}