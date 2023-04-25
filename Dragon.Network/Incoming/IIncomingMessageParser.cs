namespace Dragon.Network.Incoming;

public interface IIncomingMessageParser {
    IConnectionRepository? ConnectionRepository { get; init; }
    void Process(IConnection connection, object packet);
}