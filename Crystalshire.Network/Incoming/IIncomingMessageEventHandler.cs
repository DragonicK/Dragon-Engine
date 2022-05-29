using Disruptor;
using Crystalshire.Network.Messaging;

namespace Crystalshire.Network.Incoming {
    public interface IIncomingMessageEventHandler : IEventHandler<RingBufferByteArray> {
        IMessageRepository<MessageHeader> MessageRepository { get; }
        IIncomingMessageParser IncomingMessageParser { get; }
    }
}