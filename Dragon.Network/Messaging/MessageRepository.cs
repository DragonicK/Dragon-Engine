using System.Reflection;

namespace Dragon.Network.Messaging;

public class MessageRepository : IMessageRepository<MessageHeader> {

    public IDictionary<MessageHeader, Type> Messages { get; private set; }

    public MessageRepository() {
        Messages = new Dictionary<MessageHeader, Type>();

        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly
            .GetTypes()
            .Where(t => t.GetInterface("IMessagePacket") is not null)
            .ToArray();

        foreach (var type in types) {
            var instance = Activator.CreateInstance(type) as IMessagePacket;

            if (instance is not null) {
                var header = type.GetRuntimeProperty("Header")?.GetValue(instance);

                if (header is not null) {
                    Messages.Add((MessageHeader)header, type);
                }
            }
        }
    }

    public bool Contains(MessageHeader header) {
        return Messages.ContainsKey(header);
    }

    public Type GetMessage(MessageHeader header) {
        return Messages[header];
    }
}