using Dragon.Core.Logs;

using Dragon.Network.Pool;
using Dragon.Network.Messaging;

using System.Diagnostics;

namespace Dragon.Network.Incoming;

public sealed class IncomingMessageEventHandler : IIncomingMessageEventHandler {
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
        var pool = buffer.BufferReader;
        
        buffer.Reset();

        var crypto = connection.CryptoEngine;

        if (crypto.Decipher(pool!.Content, 0, pool.Length)) {
            var value = BitConverter.ToInt32(pool.Content, 0);

            if (Enum.IsDefined(typeof(MessageHeader), value)) {
                Execute(value, pool, connection);
            }
            else {
                WriteWarning(value, pool.Length, connection);
            }
        }
        else {
            var header = BitConverter.ToInt32(pool.Content, 0);

            Logger.Warning("Invalid CheckSum", $"From Id: {connection.Id} Header: {header} Length: {pool.Length} ");
        }
    }

    private void Execute(int value, IEngineBufferReader buffer, IConnection connection) {
        var header = (MessageHeader)value;

        if (MessageRepository.Contains(header)) {
            try {
                var type = MessageRepository.GetMessage(header);
                var packet = Serializer.Deserialize(buffer, type);

                IncomingMessageParser.Process(connection, packet);
            }
            catch (Exception ex) {
                Logger.Error("Packet Serializer", $"Header: {header}");
                Logger.Error("Packet Serializer", $"{ex.Source}");
                Logger.Error("Packet Serializer", $"{ex.Message}");
                Logger.Error("Packet Serializer", $"{ex.StackTrace}");
            }
        }
    }

    private void WriteWarning(int value, int length, IConnection connection) {
        if (Debugger.IsAttached) {
            Logger.Warning("Invalid Header Received", $"From Id: {connection.Id} Header: {value} Length: {length} ");
        }
    }
}