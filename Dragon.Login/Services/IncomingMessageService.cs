using System.Reflection;

using Dragon.Network;
using Dragon.Network.Incoming;
using Dragon.Network.Messaging;

using Dragon.Core.Services;

using Dragon.Login.Network;

namespace Dragon.Login.Services;

public sealed class IncomingMessageService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IMessageRepository<MessageHeader>? MessageRepository { get; private set; }
    public IIncomingMessageParser? IncomingMessageParser { get; private set; }
    public IIncomingMessageQueue? IncomingMessageQueue { get; private set; }
    public IIncomingMessageEventHandler? IncomingMessageEventHandler { get; private set; }
    public IPacketRouter? PacketRouter { get; private set; }
    public IServiceContainer? ServiceContainer { get; private set; }
    public IServiceInjector? ServiceInjector { get; private set; }
    public ISerializer? Serializer { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }

    public void Start() {
        Serializer = new MessageSerializer();
        MessageRepository = new MessageRepository();
        ServiceInjector = new ServiceInjector(ServiceContainer!);

        CreatePacketRouter();

        IncomingMessageParser = new IncomingMessageParser() {
            ConnectionRepository = ConnectionService!.ConnectionRepository,
            PacketRouter = PacketRouter
        };

        IncomingMessageEventHandler = new IncomingMessageEventHandler(MessageRepository, IncomingMessageParser, Serializer, LoggerService.Logger);
        IncomingMessageQueue = new IncomingMessageQueue(IncomingMessageEventHandler);

        IncomingMessageQueue.Start();
    }

    public void Stop() {
        IncomingMessageQueue?.Stop();
    }

    private void CreatePacketRouter() {
        var routes = GetRoutedTypes();
        var messages = MessageRepository!.Messages;

        PacketRouter = new PacketRouter();

        if (routes is not null) {
            foreach (var (header, type) in messages) {
                var route = GetRouteFromMessage(header, routes);

                if (route is not null) {
                    PacketRouter.Add(type, route);
                }
            }
        }
    }

    private IPacketRoute? GetRouteFromMessage(MessageHeader header, Type[] routes) {
        foreach (var route in routes) {
            var instance = Activator.CreateInstance(route, ServiceInjector) as IPacketRoute;

            if (instance is not null) {
                if (instance.Header == header) {
                    return instance;
                }
            }
        }

        return null;
    }

    private Type[]? GetRoutedTypes() {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null) {
            return null;
        }

        return assembly
            .GetTypes()
            .Where(t => t.IsClass && t.GetInterface(nameof(IPacketRouter)) is not null)
            .ToArray();
    }
}