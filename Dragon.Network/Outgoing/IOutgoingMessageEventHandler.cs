using Disruptor;

namespace Dragon.Network.Outgoing;
public interface IOutgoingMessageEventHandler : IEventHandler<RingBufferByteArray> {
    IOutgoingMessagePublisher OutgoingMessagePublisher { get; }
}