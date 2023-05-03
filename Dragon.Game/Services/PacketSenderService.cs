using Dragon.Core.Services;

using Dragon.Game.Network;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Services;

public sealed class PacketSenderService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public IPacketSender? PacketSender { get; private set; }
    public IServiceInjector? ServiceInjector { get; private set; }

    public void Start() {
        PacketSender = new PacketSender(ServiceInjector!);
    }

    public void Stop() {

    }
}