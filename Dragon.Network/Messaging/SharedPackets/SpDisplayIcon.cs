using Dragon.Core.Model.DisplayIcon;
using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpDisplayIcon : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DisplayIcon;
    public int Index { get; set; }
    public DisplayIconTarget DisplayTarget { get; set; }
    public DataDisplayIcon Icon { get; set; }
}