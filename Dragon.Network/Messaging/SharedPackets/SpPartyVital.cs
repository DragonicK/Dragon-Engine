namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPartyVital : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyVital;
    public int Index { get; set; }
    public int[] Vital { get; set; } = Array.Empty<int>();
    public int[] MaximumVital { get; set; } = Array.Empty<int>();
}