using Disruptor;
using Dragon.Network.Messaging;

namespace Dragon.Network.Incoming;
public interface IIncomingMessageEventHandler : IEventHandler<RingBufferByteArray> {
    IMessageRepository<MessageHeader> MessageRepository { get; }
    IIncomingMessageParser IncomingMessageParser { get; }
}