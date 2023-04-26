using Dragon.Network;

namespace Dragon.Login.Network;

public sealed class PacketRouter : IPacketRouter {
    private readonly Dictionary<Type, IPacketRoute> routes;

    public PacketRouter() {
        routes = new Dictionary<Type, IPacketRoute>();
    }

    public void Add(Type key, IPacketRoute value) => routes.Add(key, value);

    private bool Contains(object packet) => routes.ContainsKey(packet.GetType());

    public void Process(IConnection connection, object packet) {
        if (Contains(packet)) {
            var route = routes[packet.GetType()];

            route.Process(connection, packet);
        }
    }
}