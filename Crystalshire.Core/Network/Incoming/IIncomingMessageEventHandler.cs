using Disruptor;
using Crystalshire.Core.Network.Messaging;

namespace Crystalshire.Core.Network.Incoming {
    public interface IIncomingMessageEventHandler : IEventHandler<RingBufferByteArray> {
        IMessageRepository<MessageHeader> MessageRepository { get; }
        IIncomingMessageParser IncomingMessageParser { get; }
    }
}