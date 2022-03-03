using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpCharacters : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Characters;
        public DataCharacter[] Characters { get; set; } = Array.Empty<DataCharacter>(); 
    }
}