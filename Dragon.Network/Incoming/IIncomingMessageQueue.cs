using Dragon.Network.Pool;

namespace Dragon.Network.Incoming;

public interface IIncomingMessageQueue {
    IIncomingMessageEventHandler IncomingMessageEventHandler { get; }
    void Start();
    void Stop();
    void Enqueue(IConnection connection, int fromId, IEngineBuffer sequence);
}