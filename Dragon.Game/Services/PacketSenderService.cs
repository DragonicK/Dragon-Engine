using Dragon.Core.Services;

using Dragon.Game.Network;

namespace Dragon.Game.Services;

public class PacketSenderService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public IPacketSender? PacketSender { get; private set; }
    public ConfigurationService? ConfigurationService { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PassphraseService? PassphraseService { get; private set; }

    public void Start() {
        PacketSender = new PacketSender() {
            InstanceService = InstanceService,
            Configuration = ConfigurationService,
            PassphraseService = PassphraseService,
            Writer = OutgoingMessageService!.OutgoingMessageWriter
        };
    }

    public void Stop() {

    }
}