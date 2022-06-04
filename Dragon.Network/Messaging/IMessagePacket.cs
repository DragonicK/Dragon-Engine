namespace Dragon.Network.Messaging;

public interface IMessagePacket {
    MessageHeader Header { get; }
}