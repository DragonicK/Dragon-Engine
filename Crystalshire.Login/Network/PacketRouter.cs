using Crystalshire.Core.Network;
using Crystalshire.Core.Services;

namespace Crystalshire.Login.Network {
    public class PacketRouter : IPacketRouter {
        private readonly Dictionary<Type, Type> routes;
        private readonly IServiceInjector injector;

        public PacketRouter(IServiceContainer container) {
            routes = new Dictionary<Type, Type>();
            injector = new ServiceInjector(container);
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
        }
    }
}
