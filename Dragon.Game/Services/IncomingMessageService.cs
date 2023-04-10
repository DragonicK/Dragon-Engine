using System.Reflection;

using Dragon.Network;
using Dragon.Network.Incoming;
using Dragon.Network.Messaging;

using Dragon.Core.Services;
using Dragon.Game.Network;

namespace Dragon.Game.Services;

public class IncomingMessageService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IMessageRepository<MessageHeader>? MessageRepository { get; private set; }
    public IIncomingMessageParser? IncomingMessageParser { get; private set; }
    public IIncomingMessageQueue? IncomingMessageQueue { get; private set; }
    public IIncomingMessageEventHandler? IncomingMessageEventHandler { get; private set; }
    public IPacketRouter? PacketRouter { get; private set; }
    public IServiceContainer? Services { get; private set; }
    public ISerializer? Serializer { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public LoggerService? LoggerService { get; private set; }

    public void Start() {
        Serializer = new MessageSerializer();
        MessageRepository = new MessageRepository();

        CreatePacketRouter();

        IncomingMessageParser = new IncomingMessageParser() {
            ConnectionRepository = ConnectionService!.ConnectionRepository,
            PacketRouter = PacketRouter
        };

        IncomingMessageEventHandler = new IncomingMessageEventHandler(MessageRepository, 
                                                        IncomingMessageParser,
                                                        Serializer,
                                                        LoggerService!.Logger!);

        IncomingMessageQueue = new IncomingMessageQueue(IncomingMessageEventHandler);

        IncomingMessageQueue.Start();
    }

    public void Stop() {
        IncomingMessageQueue?.Stop();
    }

    private void CreatePacketRouter() {
        var types = GetTypes();
        var messages = MessageRepository!.Messages;

        PacketRouter = new PacketRouter(Services!, LoggerService!.Logger!);

        foreach (var (_, type) in messages) {
            AddTypeThatHasProperty(types!, type);
        }
    }

    private void AddTypeThatHasProperty(Type[] types, Type property) {
        foreach (var type in types) {
            if (GetPropertyFromType(type, property) is not null) {
                PacketRouter?.Add(property, type);
            }
        }
    }

    private static PropertyInfo? GetPropertyFromType(Type type, Type property) {
        return type.GetProperties().Where(p => p.PropertyType.Equals(property)).FirstOrDefault();
    }

    private Type[]? GetTypes() {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null) {
            return null;
        }

        return assembly
            .GetTypes()
            .Where(t => t.IsClass)
            .ToArray();
    }
}