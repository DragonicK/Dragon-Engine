using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeAttribute : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetAttribute;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 3;

    public ChangeAttribute(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(administrator, parameters);
            }
        }
    }

    private void ContinueProcess(IPlayer administrator, string[] parameters) {
        if (administrator.AccountLevel >= AccountLevel.Superior) {
            int.TryParse(parameters[1], out var id);
            int.TryParse(parameters[2], out var value);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Set(administrator, target, GetAttribute(id), value);
        }
    }

    private void Set(IPlayer administrator, IPlayer? player, PrimaryAttribute id, int value) {
        var sender = GetPacketSender();

        if (player is not null) {
            player.PrimaryAttributes.Set(id, value);

            player.AllocateAttributes();

            sender.SendAttributes(player);
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private PrimaryAttribute GetAttribute(int id) {
        if (Enum.IsDefined(typeof(PrimaryAttribute), id)) {
            return (PrimaryAttribute)id;
        }

        return PrimaryAttribute.Strength;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}