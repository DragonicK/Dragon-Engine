namespace Dragon.Network.Incoming;

public interface IIncomingMessageParser {
    IConnectionRepository? ConnectionRepository { get; init; }
    IPacketRouter? PacketRouter { get; init; }
    void Process(IConnection connection, dynamic packet);
}