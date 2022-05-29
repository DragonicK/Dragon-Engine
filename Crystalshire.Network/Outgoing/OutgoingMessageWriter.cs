using Crystalshire.Core.Serialization;

namespace Crystalshire.Network.Outgoing {
    public class OutgoingMessageWriter : IOutgoingMessageWriter {
        public IOutgoingMessageQueue OutgoingMessageQueue { get; }
        public ISerializer Serializer { get; }

        public OutgoingMessageWriter(IOutgoingMessageQueue outgoingMessageQueue, ISerializer serializer) {
            OutgoingMessageQueue = outgoingMessageQueue;
            Serializer = serializer;
        }

        public RingBufferByteArray CreateMessage(object packet) {
            var ringbuffer = new RingBufferByteArray();

            ringbuffer.SetContent(Serializer.Serialize(packet));
            return ringbuffer;
        }

        public void Enqueue(RingBufferByteArray buffer) {
            OutgoingMessageQueue.Enqueue(buffer);
        }
    }
}