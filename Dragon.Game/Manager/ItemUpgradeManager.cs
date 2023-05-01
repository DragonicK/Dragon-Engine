using Dragon.Core.Model;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Upgrades;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Configurations;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public class ItemUpgradeManager {
    public IPlayer? Player { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public ContentService? ContentService { get; init; }

    private const int NextLevel = 1;

    public void SendUpgradeData(int inventoryIndex) {
        var item = GetItemFromInventory(inventoryIndex);

        if (item is not null) {
            var upgrade = GetUpgrade(item);

            if (upgrade is not null) {
                ContinueSendData(upgrade, inventoryIndex);
            }
            else {
                PacketSender!.SendMessage(SystemMessage.ItemCannotBeUpgraded, QbColor.BrigthRed, Player!);
            }
        }
    }

    private void ContinueSendData(Upgrade upgrade, int inventoryIndex) {
        var inventory = Player!.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var level = inventory.Level + NextLevel;

            if (level <= upgrade.MaximumLevel) {
                var content = upgrade.ContentLevel[level];

                PacketSender!.SendUpgradeData(Player, inventoryIndex, content);
            }
            else {
                PacketSender!.SendUpgradeData(Player, inventoryIndex);
            }
        }
    }

    public void StartUpgrade(int inventoryIndex) {
        var item = GetItemFromInventory(inventoryIndex);

        if (item is not null) {
            var upgrade = GetUpgrade(item);

            if (upgrade is not null) {
                ContinueUpgrade(item, upgrade, inventoryIndex);
            }
            else {
                PacketSender!.SendMessage(SystemMessage.ItemCannotBeUpgraded, QbColor.BrigthRed, Player!);
            }
        }
    }

    private void ContinueUpgrade(Item item, Upgrade upgrade, int inventoryIndex) {
        var inventory = Player!.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var level = inventory.Level + NextLevel;

            if (level <= upgrade.MaximumLevel) {
                var content = upgrade.ContentLevel[level];

                if (content is not null) {
                    if (HasRequirements(content) && HasGold(content)) {
                        TakeRequiredItems(content);
                        TakeCurrency(content);

                        ProcessResult(GenerateRates(content), upgrade, inventory);
                    }
                    else {
                        PacketSender!.SendMessage(SystemMessage.YouDoNotHaveEnoughMaterial, QbColor.Red, Player!);
                    }
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.UpgradeMaximumLevel, QbColor.Gold, Player!);
            }
        }
    }

    private void ProcessResult(UpgradeResult result, Upgrade upgrade, CharacterInventory inventory) {
        switch (result) {
            case UpgradeResult.Failed:
                ProcessFail(upgrade, inventory);

                break;

            case UpgradeResult.Reduce:
                ProcessReduce(upgrade, inventory);

                break;

            case UpgradeResult.Success:
                ProcessSuccess(upgrade, inventory);

                break;

            case UpgradeResult.Break:
                ProcessBreak(inventory);

                break;
        }
    }

    private void ProcessFail(Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        PacketSender!.SendInventoryUpdate(Player!, index);

        PacketSender!.SendMessage(SystemMessage.UpgradeFailed, QbColor.Red, Player!);

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            PacketSender!.SendUpgradeData(Player!, index, content);
        }
        else {
            PacketSender!.SendUpgradeData(Player!, index);
        }
    }

    private void ProcessReduce(Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        if (inventory.Level > 0) {
            inventory.DecreaseLevel();

            PacketSender!.SendInventoryUpdate(Player!, index);

            PacketSender!.SendMessage(SystemMessage.UpgradeFailedButReduced, QbColor.Red, Player!);
        }
        else {
            PacketSender!.SendMessage(SystemMessage.UpgradeFailed, QbColor.Red, Player!);
        }

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            PacketSender!.SendUpgradeData(Player!, index, content);
        }
        else {
            PacketSender!.SendUpgradeData(Player!, index);
        }
    }

    private void ProcessSuccess(Upgrade upgrade, CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        inventory.IncreaseLevel();

        PacketSender!.SendInventoryUpdate(Player!, index);

        PacketSender!.SendMessage(SystemMessage.UpgradeSuccess, QbColor.BrigthGreen, Player!);

        var content = GetNextContent(upgrade, inventory);

        if (content is not null) {
            PacketSender!.SendUpgradeData(Player!, index, content);
        }
        else {
            PacketSender!.SendUpgradeData(Player!, index);
        }
    }

    private void ProcessBreak(CharacterInventory inventory) {
        var index = inventory.InventoryIndex;

        inventory.Clear();

        PacketSender!.SendInventoryUpdate(Player!, index);
        PacketSender!.SendMessage(SystemMessage.UpgradeBreak, QbColor.Red, Player!);

        PacketSender!.SendUpgradeData(Player!, 0);
    }

    private UpgradeLevel? GetNextContent(Upgrade upgrade, CharacterInventory inventory) {
        var level = inventory.Level + NextLevel;

        if (level <= upgrade.MaximumLevel) {
            return upgrade.ContentLevel[level];
        }

        return null;
    }

    private Item? GetItemFromInventory(int inventoryIndex) {
        var inventory = Player!.Inventories.FindByIndex(inventoryIndex);

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
        var r = new Random();
        var success = r.Next(1, 100);

        if (success <= content.Success) {
            return UpgradeResult.Success;
        }
        else {
            var destroy = r.Next(1, 100);

            if (destroy <= content.Break) {
                return UpgradeResult.Break;
            }
            else {
                var reduce = r.Next(1, 100);

                if (reduce <= content.Reduce) {
                    return UpgradeResult.Reduce;
                }
            }
        }

        return UpgradeResult.Failed;
    }

    private bool HasGold(UpgradeLevel content) {
        return Player!.Currencies.Get(CurrencyType.Gold) >= content.Cost;
    }

    private bool HasRequirements(UpgradeLevel content) {
        var items = content.Requirements;

        for (var i = 0; i < items.Count; ++i) {
            if (!HasItem(items[i])) {
                return false;
            }
        }

        return true;
    }

    private bool HasItem(UpgradeRequirement required) {
        if (required.Id == 0) {
            return true;
        }

        var count = 0;
        var inventories = Player!.Inventories.ToList();

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

    private void TakeCurrency(UpgradeLevel content) {
        Player!.Currencies.Subtract(CurrencyType.Gold, content.Cost);
        PacketSender!.SendCurrencyUpdate(Player, CurrencyType.Gold);
    }

    private void TakeRequiredItems(UpgradeLevel content) {
        var items = content.Requirements;

        for (var i = 0; i < items.Count; ++i) {
            TakeRequiredItem(items[i]);
        }
    }

    private void TakeRequiredItem(UpgradeRequirement required) {
        var inventories = Player!.Inventories.ToList();
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

                PacketSender!.SendInventoryUpdate(Player, inventory.InventoryIndex);

                if (rest == 0) {
                    return;
                }
                else {
                    value = rest;
                }

            }
        }
    }
}