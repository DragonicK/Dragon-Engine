using Crystalshire.Core.Serialization;
using Crystalshire.Network.Messaging;

namespace Crystalshire.Network.Incoming {
    public class IncomingMessageEventHandler : IIncomingMessageEventHandler {
        public IMessageRepository<MessageHeader> MessageRepository { get; }
        public IIncomingMessageParser IncomingMessageParser { get; }
        public ISerializer Serializer { get; }

        // Todo Add ICryptography.

        public IncomingMessageEventHandler(
            IMessageRepository<MessageHeader> messageRepository,
            IIncomingMessageParser incomingMessageParser,
            ISerializer serializer) {

            MessageRepository = messageRepository;
            IncomingMessageParser = incomingMessageParser;
            Serializer = serializer;
        }

        public void OnEvent(RingBufferByteArray buffer, long sequence, bool endOfBatch) {
            var id = buffer.FromId;
            var bytes = new byte[buffer.Length];

            buffer.GetContent(ref bytes);
            buffer.Reset();

            // Todo Decrypt bytes

            var value = BitConverter.ToInt32(bytes, 0);

            if (Enum.IsDefined(typeof(MessageHeader), value)) {
                var header = (MessageHeader)value;
    
                if (MessageRepository.Contains(header)) {
                    var type = MessageRepository.GetMessage(header);
                    dynamic packet = Serializer.Deserialize(bytes, type);

                    IncomingMessageParser.Process(id, packet);
                }
            }
        }
    }
}