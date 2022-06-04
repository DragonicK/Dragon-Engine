namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpWithdrawItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.WithdrawItem;
    public int WarehouseIndex { get; set; }
    public int Amount { get; set; }
}