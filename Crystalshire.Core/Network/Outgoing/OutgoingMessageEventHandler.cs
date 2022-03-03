namespace Crystalshire.Core.Network.Outgoing {
    public class OutgoingMessageEventHandler : IOutgoingMessageEventHandler {

        public IOutgoingMessagePublisher OutgoingMessagePublisher { get; }
        
        // Todo Add ICryptography

        public OutgoingMessageEventHandler(IOutgoingMessagePublisher outgoingMessagePublisher) {
            OutgoingMessagePublisher = outgoingMessagePublisher;
        }

        public void OnEvent(RingBufferByteArray buffer, long sequence, bool endOfBatch) {
            var bytes = new byte[buffer.Length];

            buffer.GetContent(ref bytes);

            // Encrypt Bytes

            var msg = new ByteBuffer(buffer.Length + 4);
            msg.Write(bytes.Length);
            msg.Write(bytes);
 
            OutgoingMessagePublisher.Broadcast(
                buffer.TransmissionTarget,
                buffer.DestinationPeers,
                buffer.ExceptDestination,
                msg.ToArray()
                );

            buffer.Reset();
        }
    }
}