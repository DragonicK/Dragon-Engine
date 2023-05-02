using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class AddItem : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.AddItem;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 5;

    public AddItem(IServiceInjector injector) {
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
            _ = int.TryParse(parameters[3], out var level);
            _ = bool.TryParse(parameters[4], out var bound);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Add(administrator, target, id, value, level, bound);
        }
    }

    private void Add(IPlayer administrator, IPlayer? player, int id, int value, int level, bool bound) {
        var sender = GetPacketSender();

        var items = ContentService!.Items;

        if (!items.Contains(id)) {
            return;
        }

        if (player is not null) {
            var maximum = player.Character.MaximumInventories;
            var inventory = player.Inventories.FindFreeInventory(maximum);

            if (inventory is not null) {
                if (value <= 0) {
                    value = 1;
                }

                inventory.ItemId = id;
                inventory.Value = value;
                inventory.Level = level;
                inventory.Bound = bound;

                sender.SendInventoryUpdate(player, inventory.InventoryIndex);
            }
            else {
                sender.SendMessage(SystemMessage.InventoryFull, QbColor.Red, administrator);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}