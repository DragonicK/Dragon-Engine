using Dragon.Network;
using Dragon.Core.Services;
using Dragon.Core.Logs;

namespace Dragon.Game.Network;

public class PacketRouter : IPacketRouter {
    private readonly Dictionary<Type, Type> routes;
    private readonly IServiceInjector injector;
    private readonly ILogger _logger;

    public PacketRouter(IServiceContainer container, ILogger logger) {
        routes = new Dictionary<Type, Type>();
        injector = new ServiceInjector(container);
        _logger = logger;
    }

    public void Add(Type key, Type value) => routes.Add(key, value);

    private bool Contains(dynamic packet) => routes.ContainsKey(packet.GetType());

    public void Process(IConnection connection, dynamic packet) {
        if (Contains(packet)) {
            dynamic created = Activator.CreateInstance(routes[packet.GetType()]);

            injector.Inject(created);

            created.Connection = connection;
            created.Packet = packet;

            created.Process();
        }
        else {
            _logger.Warning("Packet Not Registered", $"From Id: {connection.Id} Header: {packet.Header}");
        }
    }
}