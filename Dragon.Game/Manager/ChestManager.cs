using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Drops;
using Dragon.Core.Model.Chests;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Repository;
using Dragon.Game.Configurations;
using Dragon.Game.Instances.Chests;
using Dragon.Game.Configurations.Data;
using System.Reflection;

namespace Dragon.Game.Manager;

public class ChestManager {
    public IPlayer? Player { get; init; }
    public IDatabase<Drop>? Drops { get; init; }
    public IDatabase<Chest>? Chests { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }
    public IPlayerRepository? PlayerRepository { get; init; }

    private readonly Random random;

    public ChestManager() {
        random = new Random();
    }

    #region Create Instance Chest 

    public IInstanceChest CreateInstanceChest(IInstanceEntity entity, IInstance instance) {
        var id = entity.Id;

        var instanceChest = new InstanceChest(instance) {
            X = entity.X,
            Y = entity.Y
        };

        if (Drops!.Contains(id)) {
            var drops = Drops[id];

            if (drops!.Chests.Count > 0) {
                var chest = GetChest(drops);

                if (chest is not null) {
                    instanceChest.Chest = chest;
                    instanceChest.PartyId = Player!.PartyId;
                    instanceChest.CreateFromCharacterId = Player.Character.CharacterId;
                    instanceChest.RemainingTime = Configuration!.ChestDrop.MonsterDuration;

                    AddItems(chest, instanceChest);
                }
            }

            return instanceChest;
        }

        return instanceChest;
    }

    private void AddItems(Chest chest, IInstanceChest instanceChest) {
        foreach (var item in chest.Items) {
            var sequence = random.NextDouble();

            if (sequence <= item.Chance) {
                var created = CreateInstanceItem(item);

                if (created is not null) {
                    instanceChest.Items.Add(created);
                }
            }

            if (instanceChest.Items.Count >= chest.MaximumDisplayedItems) {
                return;
            }
        }
    }

    private IInstanceChestItem? CreateInstanceItem(ChestItem item) {
        var selected = new InstanceChestItem() {
            Id = item.Id,
            Value = item.Value,
            Level = item.Level,
            Bound = item.Bound,
            UpgradeId = item.UpgradeId,
            AttributeId = item.AttributeId
        };

        if (item.ContentType == ChestContentType.Currency) {
            if (Enum.IsDefined(typeof(CurrencyType), item.Id)) {
                selected.IsCurrency = true;
            }
            else {
                return null;
            }
        }

        return selected;
    }

    private Chest? GetChest(Drop drops) {
        var id = drops.Chests.Count == 1 ? drops.Chests[0] : 0;

        if (id == 0) {
            var count = drops.Chests.Count;
            var index = random.Next(0, count);

            id = drops.Chests[index];
        }

        if (Chests!.Contains(id)) {
            return Chests[id];
        }

        return null;
    }

    #endregion

    #region Open Chest

    public void OpenChest(int index) {
        var instance = GetInstance();

        if (instance is not null) {
            var chest = instance.GetChest(index);

            if (chest is not null) {
                if (CanOpenChest(chest)) {
                    chest.OpenedByCharacterId = Player!.Character.CharacterId;
                    chest.State = ChestState.Open;

                    Player.Target = chest;
                    Player.TargetType = TargetType.Chest;

                    PacketSender!.SendUpdateChestState(instance, chest);
                    PacketSender!.SendChestItems(Player, chest);
                }
            }
        }
    }

    private bool CanOpenChest(IInstanceChest chest) {
        if (chest.PartyId != Player!.PartyId) {
            PacketSender!.SendMessage(SystemMessage.ChestDoesNotBelongYou, QbColor.BrigthRed, Player);

            return false;
        }
        else if (Player.Character.CharacterId != chest.CreateFromCharacterId) {
            PacketSender!.SendMessage(SystemMessage.ChestDoesNotBelongYou, QbColor.BrigthRed, Player);

            return false;
        }

        if (chest.OpenedByCharacterId > 0) {
            if (chest.OpenedByCharacterId != Player.Character.CharacterId) {
                PacketSender!.SendMessage(SystemMessage.ChestIsOpenedByAnotherPlayer, QbColor.BrigthRed, Player);

                return false;
            }
        }

        return true;
    }

    #endregion

    #region Close Chest

    public void CloseChest() {
        if (Player!.TargetType == TargetType.Chest) {
            var chest = Player.Target as IInstanceChest;

            if (chest is not null) {
                chest.OpenedByCharacterId = 0;
            }
        }
    }

    #endregion

    #region Take Item From Chest

    public void TakeItem(int index) {
        if (Player!.TargetType == TargetType.Chest) {
            var chest = Player.Target as IInstanceChest;

            if (chest is not null) {
                if (index > chest.Items.Count) {
                    return;
                }

                if (chest.OpenedByCharacterId > 0) {
                    if (chest.OpenedByCharacterId != Player.Character.CharacterId) {
                        PacketSender!.SendMessage(SystemMessage.ChestIsOpenedByAnotherPlayer, QbColor.BrigthRed, Player);
                        return;
                    }

                    index--;

                    var canTakeItem = false;
                    var item = chest.Items[index];

                    if (item.IsCurrency) {
                        canTakeItem = TakeCurrency(item);
                    }
                    else {
                        canTakeItem = TakeItem(chest, item);
                    }

                    if (canTakeItem) {
                        chest.Items.RemoveAt(index);

                        PacketSender!.SendSortChestItemList(Player, index);
                    }
                    else {
                        PacketSender!.SendEnableTakeItemFromChest(Player);
                    }

                    var instance = GetInstance();

                    CheckForEmptyChest(instance, chest);
                }
            }
        }
    }

    private void CheckForEmptyChest(IInstance? instance, IInstanceChest chest) {
        if (chest.Items.Count == 0) {
            var id = chest.OpenedByCharacterId;

            chest.State = ChestState.Empty;

            if (id > 0) {
                var player = PlayerRepository!.FindByCharacterId(id);

                if (player is not null) {
                    player.Target = null;
                    player.TargetType = TargetType.None;

                    PacketSender!.SendCloseChest(player, chest);
                    PacketSender!.SendTarget(player, TargetType.None, 0);
                }
            }

            if (instance is not null) {
                instance.Remove(chest);

                PacketSender!.SendUpdateChestState(instance, chest);
            }
        }
    }

    #endregion

    #region Take Item

    private bool TakeItem(IInstanceChest chest, IInstanceChestItem item) {
        if (item.CharacterIdFromRollDiceWinner > 0) {
            if (Environment.TickCount >= item.WinnerTimeLimit) {
                item.CharacterIdFromRollDiceWinner = 0;

                return false;
            }

            var winner = PlayerRepository!.FindByCharacterId(item.CharacterIdFromRollDiceWinner);

            if (winner is not null) {
                return AddItem(winner, item);
            }
        }
        else {
            if (chest.PartyId > 0) {
                //// Se estiver um grupo, adiciona na lista de rolagem.
                //if (PartyCollectedItem.AddCollectedItem(player, corpse, drop)) {
                //    return;
                //}
            }
            else {
                return AddItem(Player, item);
            }
        }

        return false;
    }

    private bool AddItem(IPlayer player, IInstanceChestItem item) {
        var empty = player!.Inventories.FindFreeInventory(player.Character.MaximumInventories);

        if (empty is not null) {
            empty.ItemId = item.Id;
            empty.Value = item.Value;
            empty.Level = item.Level;
            empty.Bound = item.Bound;
            empty.UpgradeId = item.UpgradeId;
            empty.AttributeId = item.AttributeId;

            PacketSender!.SendInventoryUpdate(player, empty.InventoryIndex);
            PacketSender!.SendMessage(SystemMessage.ReceivedItem, QbColor.BrigthCyan, player, new string[] { item.Id.ToString(), item.Value.ToString() });

            return true;
        }
        else {
            PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
        }

        return false;
    }


    #endregion

    #region Take Currency

    private bool TakeCurrency(IInstanceChestCurrency item) {
        var added = false;
        var rest = 0;

        if (Player!.PartyId > 0) {
            if (!CanSharePartyCurrency(item)) {
                added = CanAddCurrency(Player, item.Currency, item.Value, out rest);
            }
        }
        else {
            added = CanAddCurrency(Player, item.Currency, item.Value, out rest);
        }

        if (added) {
            PacketSender!.SendCurrencyUpdate(Player, item.Currency);
            PacketSender!.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, Player, new string[] { ((int)item.Currency).ToString(), item.Value.ToString() });
        }

        if (added && rest > 0) {
            item.Value = rest;

            PacketSender!.SendUpdateChestItem(Player, (IInstanceChestItem)item);

            return false;
        }
        else if (added && rest < 1) {
            return true;
        }

        return false;
    }

    private bool CanSharePartyCurrency(IInstanceChestCurrency item) {
        var currentMapId = Player!.Character.Map;
        var party = GetParty();

        if (party is not null) {
            var members = party.Members;
            var totalPlayers = 0;

            foreach (var member in members) {
                if (member?.Player is not null) {
                    if (!member.Disconnected) {
                        if (currentMapId == member.Player.Character.Map) {
                            totalPlayers++;
                        }
                    }
                }
            }

            if (totalPlayers > 0) {
                if (PartyShareCurrency(item.Currency, item.Value, party, totalPlayers, out var rest)) {
                    var module = item.Value % totalPlayers;

                    if (module > 0) {
                        if (CanAddCurrency(Player, item.Currency, module, out _)) {
                            PacketSender!.SendCurrencyUpdate(Player, item.Currency);
                            PacketSender!.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, Player, new string[] { ((int)item.Currency).ToString(), module.ToString() });
                        }                   
                    }

                    if (rest > 0) {
                        return PartyShareCurrency(item.Currency, rest, party, totalPlayers, out _);
                    }

                    return true;
                }
            }
        }

        return false;
    }

    private bool PartyShareCurrency(CurrencyType currencyType, int currencyValue, PartyManager party, int totalPlayers, out int restAdded) {
        var currency = currencyValue / totalPlayers;
        var currentMapId = Player!.Character.Map;
        var totalRest = 0;

        if (currency > 0) {
            var members = party.Members;

            foreach (var member in members) {
                if (member?.Player is not null) {
                    if (!member.Disconnected) {
                        if (currentMapId == member.Player.Character.Map) {
                            var player = member.Player;

                            if (CanAddCurrency(player, currencyType, currencyValue, out var rest)) {
                                PacketSender!.SendCurrencyUpdate(player, currencyType);
                                PacketSender!.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, player, new string[] { ((int)currencyType).ToString(), (currencyValue - rest).ToString() });
                            }
                            else {
                                PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, player);
                            }

                            totalRest += rest;
                        }
                    }
                }
            }

            restAdded = totalRest;

            return true;
        }

        restAdded = totalRest;

        return false;
    }

    private bool CanAddCurrency(IPlayer player, CurrencyType currencyType, int currencyValue, out int rest) {
        if (!player.Currencies.Add(currencyType, currencyValue)) {
            var currency = player.Currencies.GetCurrency(currencyType);
            var valueToReachMaximum = int.MaxValue - currency.CurrencyValue;

            if (valueToReachMaximum < 1) {
                rest = 0;

                return false;
            }

            rest = currencyValue - valueToReachMaximum;

            return player.Currencies.Add(currencyType, valueToReachMaximum);
        }

        rest = 0;

        return true;
    }

    #endregion

    private IInstance? GetInstance() {
        var instances = InstanceService!.Instances;
        var instanceId = Player!.Character.Map;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }

    private PartyManager? GetParty() {
        var parties = InstanceService!.Parties;
        var partyId = Player!.PartyId;

        if (parties.ContainsKey(partyId)) {
            return parties[partyId];
        }

        return null;
    }
}