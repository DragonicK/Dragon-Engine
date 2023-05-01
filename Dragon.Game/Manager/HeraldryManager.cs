using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class HeraldryManager {
    public ContentService? ContentService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }
    public ConfigurationService? ConfigurationService { get; private set; }

    private const int MaximumAttempts = 10;

    public HeraldryManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void UnequipHeraldry(IPlayer player, int index) {
        var sender = GetPacketSender();
        var heraldry = player.Heraldries.Get(index);

        if (heraldry is not null) {
            if (heraldry.ItemId > 0) {
                var maximum = player.Character.MaximumInventories;
                var inventory = player.Inventories.FindFreeInventory(maximum);

                if (inventory is not null) {
                    inventory.Apply(heraldry);

                    player.Heraldries.Unequip(index);

                    sender.SendInventoryUpdate(player, inventory.InventoryIndex);
                    sender.SendHeraldryUpdate(player, index);

                    SendAttributes(sender, player);
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.Red, player);
                }
            }
        }
    }

    public void EquipHeraldry(IPlayer player, int index, Item item) {
        var sender = GetPacketSender();

        if (item.EquipmentId > 0) {
            var inventory = player.Inventories.FindByIndex(index)!;

            if (inventory.AttributeId == 0) {
                if (CanRevealHeraldryAttribute(sender, player, inventory, item)) {
                    sender!.SendInventoryUpdate(player, index);
                }
            }
            else {
                var maximum = Configuration!.Player.MaximumHeraldries;
                var heraldry = player!.Heraldries.FindFreeHeraldry(maximum);

                if (heraldry is not null) {
                    ContinueEquipHeraldryAtIndex(sender, player, heraldry.InventoryIndex, index);
                }
            }
        }
    }

    public void EquipHeraldryAtIndex(IPlayer player, int heraldryIndex, int index) {
        var sender = GetPacketSender();

        var item = GetItemFromInventory(player, index);

        if (item is not null) {

            if (item.ClassCode != player!.Character.ClassCode) {
                var parameters = new string[] { item.ClassCode.ToString() };

                sender.SendMessage(SystemMessage.OnlyClassCodeCanUseItem, QbColor.Red, player, parameters);
            }
            else {
                if (item!.RequiredLevel > player.Character.Level) {
                    sender.SendMessage(SystemMessage.YouDoNotHaveRequiredLevel, QbColor.Red, player);
                }
                else {
                    if (item.Type == ItemType.Heraldry) {
                        ContinueEquipHeraldryAtIndex(sender, player, heraldryIndex, index);
                    }
                }
            }
        }
    }

    private void ContinueEquipHeraldryAtIndex(IPacketSender sender, IPlayer player, int heraldryIndex, int index) {
        var inventory = player!.Inventories.FindByIndex(index);

        if (inventory!.AttributeId > 0) {
            if (player!.Heraldries.IsEquipped(heraldryIndex)) {
                SwapHeraldry(player, heraldryIndex, inventory);
            }
            else {
                player.Heraldries.Equip(heraldryIndex, inventory);
                inventory.Clear();
            }

            sender.SendHeraldryUpdate(player, heraldryIndex);
            sender.SendInventoryUpdate(player, index);

            SendAttributes(sender, player);
        }
        else {
            sender.SendMessage(SystemMessage.NeedRevealItemAttribute, QbColor.BrigthRed, player);
        }
    }

    private bool CanRevealHeraldryAttribute(IPacketSender sender, IPlayer player, CharacterInventory inventory, Item item) {
        var heraldries = GetDatabaseHeraldries();
        
        var heraldryId = item.EquipmentId;
        var success = false;

        heraldries.TryGet(heraldryId, out var heraldry);

        if (heraldry is not null) {
            var random = new Random();
            var attempts = 0;
            var count = 0;

            if (heraldry!.Attributes.Count > 0) {
                while (!success) {
                    var chance = random.Next(1, 100);

                    if (chance <= heraldry.Attributes[count].Chance) {
                        inventory.AttributeId = heraldry.Attributes[count].AttributeId;
                        inventory.UpgradeId = heraldry.UpgradeId;
                        success = true;
                    }

                    count++;

                    if (count == heraldry.Attributes.Count) {
                        count = 0;
                    }

                    attempts++;

                    if (attempts > MaximumAttempts) {
                        break;
                    }
                }

                if (success) {
                    sender.SendMessage(SystemMessage.SuccessToRevealItemAttribute, QbColor.Red, player);
                }
                else {
                    sender.SendMessage(SystemMessage.FailedToRevealItemAttribute, QbColor.Red, player);
                }
            }
            else {
                sender.SendMessage(SystemMessage.CannotRevealItemAttribute, QbColor.Red, player);
            }
        }

        return success;
    }

    private void SwapHeraldry(IPlayer player, int index, CharacterInventory inventory) {
        var heraldry = player.Heraldries.Get(index);

        if (heraldry is not null) {
            var clone = heraldry!.Clone();

            player.Heraldries.Unequip(index);
            player.Heraldries.Equip(index, inventory);

            inventory.Clear();
            inventory.Apply(clone);
        }
    }

    private Item? GetItemFromInventory(IPlayer player, int index) {
        var items = GetDatabaseItems();

        var inventory = player.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var itemId = inventory.ItemId;

            items.TryGet(itemId, out var item);

            return item;
        }

        return null;
    }

    private void SendAttributes(IPacketSender sender, IPlayer player) {
        player.AllocateAttributes();

        sender.SendAttributes(player);

        var instance = GetInstance(player);

        if (instance is not null) {
            sender.SendPlayerVital(player, instance);
        }
        else {
            sender.SendPlayerVital(player);
        }
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Item> GetDatabaseItems() {
        return ContentService!.Items;
    }

    private IDatabase<Heraldry> GetDatabaseHeraldries() {
        return ContentService!.Heraldries;
    }
}