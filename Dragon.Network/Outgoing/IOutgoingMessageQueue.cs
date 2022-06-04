namespace Dragon.Network.Outgoing;

public interface IOutgoingMessageQueue {
    IOutgoingMessageEventHandler OutgoingMessageEventHandler { get; }
    void Start();
    void Stop();
    void Enqueue(RingBufferByteArray buffer);
}