namespace Dragon.Network.Outgoing;
public class OutgoingMessageEventHandler : IOutgoingMessageEventHandler {

    public IOutgoingMessagePublisher OutgoingMessagePublisher { get; }

    public OutgoingMessageEventHandler(IOutgoingMessagePublisher outgoingMessagePublisher) {
        OutgoingMessagePublisher = outgoingMessagePublisher;
    }

    public void OnEvent(RingBufferByteArray buffer, long sequence, bool endOfBatch) {
        var bytes = new byte[buffer.Length + 4];

        buffer.GetContent(ref bytes, 4);

        OutgoingMessagePublisher.Broadcast(
            buffer.TransmissionTarget,
            buffer.DestinationPeers,
            buffer.ExceptDestination,
            bytes
        );

        buffer.Reset();
    }
}