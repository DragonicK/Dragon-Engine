using Disruptor;

namespace Crystalshire.Core.Network.Outgoing {
    public interface IOutgoingMessageEventHandler : IEventHandler<RingBufferByteArray> {
        IOutgoingMessagePublisher OutgoingMessagePublisher { get; }
    }
}