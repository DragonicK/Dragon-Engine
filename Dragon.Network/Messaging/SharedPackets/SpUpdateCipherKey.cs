using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public class SpUpdateCipherKey : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpdateCipherKey;
    public byte[] Key { get; set; } = Array.Empty<byte>();
    public GameState GameState { get; set; }
}