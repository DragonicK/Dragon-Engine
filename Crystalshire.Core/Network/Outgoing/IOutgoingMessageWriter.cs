using Crystalshire.Core.Serialization;

namespace Crystalshire.Core.Network.Outgoing {
    public interface IOutgoingMessageWriter {
        IOutgoingMessageQueue OutgoingMessageQueue { get; }
        ISerializer Serializer { get; }
        RingBufferByteArray CreateMessage(object packet);
        void Enqueue(RingBufferByteArray buffer);
    }
}