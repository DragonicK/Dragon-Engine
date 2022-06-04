using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpParty : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Party;
    public int LeaderIndex { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int MaximumExperience { get; set; }
    public DataPartyMember[] Members { get; set; } = Array.Empty<DataPartyMember>();
}