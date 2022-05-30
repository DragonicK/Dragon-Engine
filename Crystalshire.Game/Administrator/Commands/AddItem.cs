using Crystalshire.Core.Model;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Administrator.Commands;

public sealed class AddItem : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.AddItem;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 5;

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
                int.TryParse(parameters[3], out var level);
                bool.TryParse(parameters[4], out var bound);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Add(target, id, value, level, bound);
            }
        }
    }

    private void Add(IPlayer? player, int id, int value, int level, bool bound) {
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

                PacketSender!.SendInventoryUpdate(player, inventory.InventoryIndex);
            }
            else {
                PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.Red, Administrator!);
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }
}