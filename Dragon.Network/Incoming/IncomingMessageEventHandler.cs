using Dragon.Core.Logs;

using Dragon.Network.Messaging;

using System.Diagnostics;

namespace Dragon.Network.Incoming;

public class IncomingMessageEventHandler : IIncomingMessageEventHandler {
    public IMessageRepository<MessageHeader> MessageRepository { get; }
    public IIncomingMessageParser IncomingMessageParser { get; }
    public ISerializer Serializer { get; }
    public ILogger Logger { get; }

    // Todo Add ICryptography.

    public IncomingMessageEventHandler(
        IMessageRepository<MessageHeader> messageRepository,
        IIncomingMessageParser incomingMessageParser,
        ISerializer serializer,
        ILogger logger) {

        MessageRepository = messageRepository;
        IncomingMessageParser = incomingMessageParser;
        Serializer = serializer;
        Logger = logger;
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
        else {
            if (Debugger.IsAttached) {
                Logger.Warning("Invalid Header Received", $"From Id: {id} Header: {value} Length: {bytes.Length} ");
            }
        }
    }
}