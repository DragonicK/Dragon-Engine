using Dragon.Core.Model.DisplayIcon;

using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpDisplayIcons : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DisplayIcons;
    public int Index { get; set; }
    public DisplayIconTarget DisplayTarget { get; set; }
    public DataDisplayIcon[] Icons { get; set; } = Array.Empty<DataDisplayIcon>();
}