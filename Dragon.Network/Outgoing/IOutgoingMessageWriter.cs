namespace Dragon.Network.Outgoing;

public interface IOutgoingMessageWriter {
    IOutgoingMessageQueue OutgoingMessageQueue { get; }
    ISerializer Serializer { get; }
    RingBufferByteArray CreateMessage(object packet);
    void Enqueue(RingBufferByteArray buffer);
}