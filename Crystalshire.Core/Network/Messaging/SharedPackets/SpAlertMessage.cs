using Crystalshire.Core.Model;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpAlertMessage : IMessagePacket { 
        public MessageHeader Header { get; set; } = MessageHeader.AlertMessage;
        public AlertMessageType AlertMessage { get; set; }
        public MenuResetType MenuReset { get; set; }
        public bool Kick { get; set; }
        public bool Forced { get; set; }
    }
}