using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeCurrency : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetCurrency;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 4;

    public ChangeCurrency(IServiceInjector injector) {
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
            _ = int.TryParse(parameters[1], out var id);
            _ = int.TryParse(parameters[2], out var value);
            _ =int.TryParse(parameters[3], out var add);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            if (add > 0) {
                Add(administrator, target, GetCurrencyType(id), value);
            }
            else {
                Set(administrator, target, GetCurrencyType(id), value);
            }
        }
    }

    private void Set(IPlayer administrator, IPlayer? player, CurrencyType type, int value) {
        var sender = GetPacketSender();

        if (player is not null) {
            player.Currencies.Set(type, value);

            sender.SendCurrencyUpdate(player, type);
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private void Add(IPlayer administrator, IPlayer? player, CurrencyType type, int value) {
        var sender = GetPacketSender();

        if (player is not null) {
            player.Currencies.Add(type, value);

            sender.SendCurrencyUpdate(player, type);
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private CurrencyType GetCurrencyType(int id) {
        if (Enum.IsDefined(typeof(CurrencyType), id)) {
            return (CurrencyType)id;
        }

        return CurrencyType.Gold;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}