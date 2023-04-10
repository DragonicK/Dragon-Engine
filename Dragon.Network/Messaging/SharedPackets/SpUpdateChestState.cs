using Dragon.Core.Model.Chests;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpUpdateChestState : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpdateChestState;
    public int Index { get; set; }
    public ChestState State { get; set; }
}