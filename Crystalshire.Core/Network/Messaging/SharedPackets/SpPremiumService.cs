using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpPremiumService : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.PremiumService;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public DataRate Rates { get; set; }
    }
}