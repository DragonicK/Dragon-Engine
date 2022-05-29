using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets {
    public sealed class SpCharacters : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Characters;
        public DataCharacter[] Characters { get; set; } = Array.Empty<DataCharacter>(); 
    }
}