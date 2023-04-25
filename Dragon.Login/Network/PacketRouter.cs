using Dragon.Network;

namespace Dragon.Login.Network;

public sealed class PacketRouter : IPacketRouter {
    private readonly Dictionary<Type, IRoute> routes;

    public PacketRouter() {
        routes = new Dictionary<Type, IRoute>();
    }

    public void Add(Type key, IRoute value) => routes.Add(key, value);

    private bool Contains(object packet) => routes.ContainsKey(packet.GetType());

    public void Process(IConnection connection, object packet) {
        if (Contains(packet)) {
            var route = routes[packet.GetType()];

            route.Process(connection, packet);
        }
    }
}