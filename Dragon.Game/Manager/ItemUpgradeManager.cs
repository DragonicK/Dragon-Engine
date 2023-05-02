using Dragon.Core.Services;

using Dragon.Core.Model;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Upgrades;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ItemUpgradeManager {
    public ContentService? ContentService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int NextLevel = 1;

    private readonly Random random;

    public ItemUpgradeManager(IServiceInjector injector) {
        injector.Inject(this);

        random = new Random();
    }

    public void SendUpgradeData(IPlayer player, int inventoryIndex) {
        var item = GetItemFromInventory(player, inventoryIndex);
        var sender = GetPacketSender();

        if (item is not null) {
            var upgrade = GetUpgrade(item);

            if (upgrade is not null) {
                ContinueSendData(sender, player, upgrade, inventoryIndex);
            }
            else {
                sender.SendMessage(SystemMessage.ItemCannotBeUpgraded, QbColor.BrigthRed, player);
            }
        }
    }

    private void ContinueSendData(IPacketSender sender, IPlayer player, Upgrade upgrade, int inventoryIndex) {
        var inventory = player.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var level = inventory.Level + NextLevel;

            if (level <= upgrade.MaximumLevel) {
                var content = upgrade.ContentLevel[level];

                sender.SendUpgradeData(player, inventoryIndex, content);
            }
            else {
                sender.SendUpgradeData(player, inventoryIndex);
            }
        }
    }

    public void StartUpgrade(IPlayer player, int inventoryIndex) {
        var item = GetItemFromInventory(player, inventoryIndex);
        
        var sender = GetPacketSender();

        if (item is not null) {
            var upgrade = GetUpgrade(item);

            if (upgrade is not null) {
                ContinueUpgrade(sender, player, upgrade, inventoryIndex);
            }
            else {
                sender.SendMessage(SystemMessage.ItemCannotBeUpgraded, QbColor.BrigthRed, player);
            }
        }
    }

    private void ContinueUpgrade(IPacketSender sender, IPlayer player, Upgrade upgrade, int inventoryIndex) {
        var inventory = player.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var level = inventory.Level + NextLevel;

            if (level <= upgrade.MaximumLevel) {
                var content = upgrade.ContentLevel[level];

                if (content is not null) {
                    if (HasRequirements(player, content) && HasGold(player, content)) {
                        TakeRequiredItems(sender, player, content);
                        TakeCurrency(sender, player, content);

                        ProcessResult(sender, player, GenerateRates(content), upgrade, inventory);
                    }
                    else {
                        sender.SendMessage(SystemMessage.YouDoNotHaveEnoughMaterial, QbColor.Red, player);
                    }
                }
            }
            else {
                sender.SendMessage(SystemMessage.UpgradeMaximumLevel, QbColor.Gold, player);
            }
        }
    }

    private void ProcessResult(IPacketSender sender, IPlayer player, UpgradeResult result, Upgrade upgrade, CharacterInventory inventory) {
        switch (result) {
            case UpgradeResult.Failed:
                ProcessFail(sender, player, upgrade, inventory);

                break;

            case UpgradeResult.Reduce:
                ProcessReduce(sender, player, upgrade, inventory);

                break;

            case UpgradeResult.Success:
                ProcessSuccess(sender, player, upgrade, inventory);

                break;

            case UpgradeResult.Break:
                ProcessBreak(sender, player, inventory);

                break;
        }
    }

    private void ProcessFail(IPacketSender sender, IPlayer player, Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        sender.SendInventoryUpdate(player, index);

        sender.SendMessage(SystemMessage.UpgradeFailed, QbColor.Red, player);

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            sender.SendUpgradeData(player, index, content);
        }
        else {
            sender.SendUpgradeData(player, index);
        }
    }

    private void ProcessReduce(IPacketSender sender, IPlayer player, Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        if (inventory.Level > 0) {
            inventory.DecreaseLevel();

            sender.SendInventoryUpdate(player, index);

            sender.SendMessage(SystemMessage.UpgradeFailedButReduced, QbColor.Red, player);
        }
        else {
            sender.SendMessage(SystemMessage.UpgradeFailed, QbColor.Red, player);
        }

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            sender.SendUpgradeData(player, index, content);
        }
        else {
            sender.SendUpgradeData(player, index);
        }
    }

    private void ProcessSuccess(IPacketSender sender, IPlayer player, Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        inventory.IncreaseLevel();

        sender.SendInventoryUpdate(player, index);

        sender.SendMessage(SystemMessage.UpgradeSuccess, QbColor.BrigthGreen, player);

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            sender.SendUpgradeData(player, index, content);
        }
        else {
            sender.SendUpgradeData(player, index);
        }
    }

    private void ProcessBreak(IPacketSender sender, IPlayer player, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        inventory.Clear();

        sender.SendInventoryUpdate(player, index);
        sender.SendMessage(SystemMessage.UpgradeBreak, QbColor.Red, player);

        sender.SendUpgradeData(player, 0);
    }

    private UpgradeLevel? GetNextContent(Upgrade upgrade, CharacterInventory inventory) {
        var level = inventory.Level + NextLevel;

        if (level <= upgrade.MaximumLevel) {
            return upgrade.ContentLevel[level];
        }

        return null;
    }

    private Item? GetItemFromInventory(IPlayer player, int inventoryIndex) {
        var inventory = player.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var items = ContentService!.Items;

            if (items.Contains(inventory.ItemId)) {
                return items[inventory.ItemId];
            }
        }

        return null;
    }

    private Upgrade? GetUpgrade(Item item) {
        var upgrades = ContentService!.Upgrades;

        if (upgrades.Contains(item.UpgradeId)) {
            return upgrades[item.UpgradeId];
        }

        return null;
    }

    private UpgradeResult GenerateRates(UpgradeLevel content) {
          var success = random.Next(1, 100);

        if (success <= content.Success) {
            return UpgradeResult.Success;
        }
        else {
            var destroy = random.Next(1, 100);

            if (destroy <= content.Break) {
                return UpgradeResult.Break;
            }
            else {
                var reduce = random.Next(1, 100);

                if (reduce <= content.Reduce) {
                    return UpgradeResult.Reduce;
                }
            }
        }

        return UpgradeResult.Failed;
    }

    private bool HasGold(IPlayer player, UpgradeLevel content) {
        return player.Currencies.Get(CurrencyType.Gold) >= content.Cost;
    }

    private bool HasRequirements(IPlayer player, UpgradeLevel content) {
        var items = content.Requirements;

        for (var i = 0; i < items.Count; ++i) {
            if (!HasItem(player, items[i])) {
                return false;
            }
        }

        return true;
    }

    private bool HasItem(IPlayer player, UpgradeRequirement required) {
        if (required.Id == 0) {
            return true;
        }

        var count = 0;
        var inventories = player.Inventories.ToList();

        for (var i = 0; i < inventories.Count; ++i) {
            if (inventories[i].ItemId == required.Id) {
                count += inventories[i].Value;

                if (count >= required.Amount) {
                    return true;
                }
            }
        }

        return false;
    }

    private void TakeCurrency(IPacketSender sender, IPlayer player, UpgradeLevel content) {
        player.Currencies.Subtract(CurrencyType.Gold, content.Cost);
        sender.SendCurrencyUpdate(player, CurrencyType.Gold);
    }

    private void TakeRequiredItems(IPacketSender sender, IPlayer player, UpgradeLevel content) {
        var items = content.Requirements;

        for (var i = 0; i < items.Count; ++i) {
            TakeRequiredItem(sender, player, items[i]);
        }
    }

    private void TakeRequiredItem(IPacketSender sender, IPlayer player, UpgradeRequirement required) {
        var inventories = player.Inventories.ToList();
        var value = required.Amount;
        int rest;

        CharacterInventory? inventory;

        for (var i = 0; i < inventories.Count; ++i) {
            inventory = inventories[i];

            if (inventory.ItemId == required.Id) {
                rest = inventory.Value - value;

                if (rest < 0) {
                    rest = value - inventory.Value;
                    inventory.Clear();
                }
                else {
                    inventory.Value -= value;
                }

                if (inventory.Value <= 0) {
                    inventory.Clear();
                }

                sender.SendInventoryUpdate(player, inventory.InventoryIndex);

                if (rest == 0) {
                    return;
                }
                else {
                    value = rest;
                }

            }    
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}