using Dragon.Core.Logs;

using Dragon.Network.Messaging;
using System.Diagnostics;

namespace Dragon.Network.Incoming;

public class IncomingMessageEventHandler : IIncomingMessageEventHandler {
    public IMessageRepository<MessageHeader> MessageRepository { get; }
    public IIncomingMessageParser IncomingMessageParser { get; }
    public ISerializer Serializer { get; }
    public ILogger Logger { get; }

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
        var connection = buffer.Connection!;
        var bytes = new byte[buffer.Length];

        buffer.GetContent(ref bytes, 0);
        buffer.Reset();

        var crypto = connection.CryptoEngine;

        if (crypto.Decipher(bytes, 0, bytes.Length)) {
            var value = BitConverter.ToInt32(bytes, 0);

            if (Enum.IsDefined(typeof(MessageHeader), value)) {
                Execute(value, bytes, connection);
            }
            else {
                WriteWarning(value, bytes, connection);
            }
        }
        else {
            Logger.Warning("Invalid CheckSum", $"From Id: {connection.Id} Length: {bytes.Length} ");
        }
    }

    private void Execute(int value, byte[] buffer, IConnection connection) {
        var header = (MessageHeader)value;

        if (MessageRepository.Contains(header)) {
            var type = MessageRepository.GetMessage(header);
            dynamic packet = Serializer.Deserialize(buffer, type);

            IncomingMessageParser.Process(connection, packet);
        }
    }

    private void WriteWarning(int value, byte[] buffer, IConnection connection) {
        if (Debugger.IsAttached) {
            Logger.Warning("Invalid Header Received", $"From Id: {connection.Id} Header: {value} Length: {buffer.Length} ");
        }
    }
}