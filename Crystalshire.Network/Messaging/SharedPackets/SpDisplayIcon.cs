using Crystalshire.Core.Model.DisplayIcon;
using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpDisplayIcon : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DisplayIcon;
    public int Index { get; set; }
    public DisplayIconTarget DisplayTarget { get; set; }
    public DataDisplayIcon Icon { get; set; }
}