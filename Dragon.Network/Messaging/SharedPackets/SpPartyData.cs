namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPartyData : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyData;
    public int LeaderIndex { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int MaximumExperience { get; set; }
}