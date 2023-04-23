namespace Dragon.Network.Outgoing;

public interface IOutgoingMessageQueue {
    IOutgoingMessageEventHandler OutgoingMessageEventHandler { get; }
    void Start();
    void Stop();
    RingBufferByteArray GetNextSequence();
    void Enqueue(RingBufferByteArray buffer);
}