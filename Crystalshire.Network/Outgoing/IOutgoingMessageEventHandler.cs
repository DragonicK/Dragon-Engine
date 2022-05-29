using Disruptor;

namespace Crystalshire.Network.Outgoing {
    public interface IOutgoingMessageEventHandler : IEventHandler<RingBufferByteArray> {
        IOutgoingMessagePublisher OutgoingMessagePublisher { get; }
    }
}