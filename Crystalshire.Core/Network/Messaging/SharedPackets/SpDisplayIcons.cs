using Crystalshire.Core.Model.DisplayIcon;
using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpDisplayIcons : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.DisplayIcons;
        public int Index { get; set; }
        public DisplayIconTarget DisplayTarget { get; set; }
        public DataDisplayIcon[] Icons { get; set; } = Array.Empty<DataDisplayIcon>();
    }
}