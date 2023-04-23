namespace Dragon.Network.Outgoing;
public class OutgoingMessageEventHandler : IOutgoingMessageEventHandler {

    public IOutgoingMessagePublisher OutgoingMessagePublisher { get; }

    public OutgoingMessageEventHandler(IOutgoingMessagePublisher outgoingMessagePublisher) {
        OutgoingMessagePublisher = outgoingMessagePublisher;
    }

    public void OnEvent(RingBufferByteArray buffer, long sequence, bool endOfBatch) {
        OutgoingMessagePublisher.Broadcast(
            buffer.TransmissionTarget,
            buffer.DestinationPeers,
            buffer.ExceptDestination,
            buffer.ByteBuffer,
            buffer.Length
        );

        buffer.Reset();
    }
}