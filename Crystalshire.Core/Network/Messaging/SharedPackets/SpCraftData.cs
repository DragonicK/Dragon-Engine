using Crystalshire.Core.Model.Crafts;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpCraftData : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.CraftData;
        public CraftType Type { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Maximum { get; set; }
    }
}