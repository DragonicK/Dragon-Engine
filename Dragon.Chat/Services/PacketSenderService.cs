using Dragon.Core.Services;

using Dragon.Chat.Network;

namespace Dragon.Chat.Services;

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