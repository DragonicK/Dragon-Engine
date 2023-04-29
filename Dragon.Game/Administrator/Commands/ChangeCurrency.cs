using Dragon.Core.Model;
using Dragon.Game.Network.Senders;
using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeCurrency : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetCurrency;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 4;

    public void Process(string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(parameters);
            }
        }
    }

    private void ContinueProcess(string[] parameters) {
        if (Administrator is not null) {
            if (Administrator.AccountLevel >= AccountLevel.Superior) {
                int.TryParse(parameters[1], out var id);
                int.TryParse(parameters[2], out var value);
                int.TryParse(parameters[3], out var add);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                if (add > 0) {
                    Add(target, GetCurrencyType(id), value);
                }
                else {
                    Set(target, GetCurrencyType(id), value);
                }
            }
        }
    }

    private void Set(IPlayer? player, CurrencyType type, int value) {
        if (player is not null) {
            player.Currencies.Set(type, value);

            PacketSender!.SendCurrencyUpdate(player, type);
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }

    private void Add(IPlayer? player, CurrencyType type, int value) {
        if (player is not null) {
            player.Currencies.Add(type, value);

            PacketSender!.SendCurrencyUpdate(player, type);
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }

    private CurrencyType GetCurrencyType(int id) {
        if (Enum.IsDefined(typeof(CurrencyType), id)) {
            return (CurrencyType)id;
        }

        return CurrencyType.Gold;
    }
}