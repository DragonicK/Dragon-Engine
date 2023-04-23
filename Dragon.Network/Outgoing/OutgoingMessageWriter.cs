namespace Dragon.Network.Outgoing;

public class OutgoingMessageWriter : IOutgoingMessageWriter {
    public IOutgoingMessageQueue OutgoingMessageQueue { get; }
    public ISerializer Serializer { get; }

    public OutgoingMessageWriter(IOutgoingMessageQueue outgoingMessageQueue, ISerializer serializer) {
        OutgoingMessageQueue = outgoingMessageQueue;
        Serializer = serializer;
    }

    public RingBufferByteArray CreateMessage(object packet) {
        var entry = OutgoingMessageQueue.GetNextSequence();

        entry.SetOutgoingContent(Serializer.Serialize(packet));

        return entry;
    }

    public void Enqueue(RingBufferByteArray buffer) {
        OutgoingMessageQueue.Enqueue(buffer);
    }
}