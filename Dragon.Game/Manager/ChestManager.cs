using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Drops;
using Dragon.Core.Model.Chests;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;
using Dragon.Game.Instances.Chests;
using Dragon.Game.Parties;

namespace Dragon.Game.Manager;

public sealed class ChestManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly Random random;

    public ChestManager(IServiceInjector injector) {
        injector.Inject(this);

        random = new Random();
    }

    #region Create Instance Chest 

    public IInstanceChest CreateInstanceChest(IPlayer player, IInstanceEntity entity, IInstance instance) {
        var id = entity.Id;

        var instanceChest = new InstanceChest(instance) {
            X = entity.X,
            Y = entity.Y
        };

        var dbDrop = GetDatabaseDrop();

        dbDrop.TryGet(id, out var drop);

        if (drop is not null) {
            if (drop.Chests.Count > 0) {
                var chest = GetChest(drop);

                if (chest is not null) {
                    instanceChest.Chest = chest;
                    instanceChest.PartyId = player.PartyId;
                    instanceChest.CreateFromCharacterId = player.Character.CharacterId;
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

    private Chest? GetChest(Drop drop) {
        var id = drop.Chests.Count == 1 ? drop.Chests[0] : 0;

        if (id == 0) {
            var count = drop.Chests.Count;
            var index = random.Next(0, count);

            id = drop.Chests[index];
        }

        var dbChest = GetDatabaseChest();

        dbChest.TryGet(id, out var chest);

        return chest;
    }

    #endregion

    #region Open Chest

    public void OpenChest(IPlayer player, int index) {
        var sender = GetPacketSender();
        var instance = GetInstance(player);

        if (instance is not null) {
            var chest = instance.GetChest(index);

            if (chest is not null) {
                if (CanOpenChest(sender, player, chest)) {
                    chest.OpenedByCharacterId = player.Character.CharacterId;
                    chest.State = ChestState.Open;

                    player.Target = chest;
                    player.TargetType = TargetType.Chest;

                    sender.SendUpdateChestState(instance, chest);
                    sender.SendChestItems(player, chest);
                }
            }
        }
    }

    private bool CanOpenChest(IPacketSender sender, IPlayer player, IInstanceChest chest) {
        if (chest.PartyId != player.PartyId) {
            sender.SendMessage(SystemMessage.ChestDoesNotBelongYou, QbColor.BrigthRed, player);

            return false;
        }
        else if (player.Character.CharacterId != chest.CreateFromCharacterId) {
            sender.SendMessage(SystemMessage.ChestDoesNotBelongYou, QbColor.BrigthRed, player);

            return false;
        }

        if (chest.OpenedByCharacterId > 0) {
            if (chest.OpenedByCharacterId != player.Character.CharacterId) {
                sender.SendMessage(SystemMessage.ChestIsOpenedByAnotherPlayer, QbColor.BrigthRed, player);

                return false;
            }
        }

        return true;
    }

    #endregion

    #region Close Chest

    public void CloseChest(IPlayer player) {
        if (player.TargetType == TargetType.Chest) {
            var chest = player.Target as IInstanceChest;

            if (chest is not null) {
                chest.OpenedByCharacterId = 0;
            }
        }
    }

    #endregion

    #region Take Item From Chest

    public void TakeItem(IPlayer player, int index) {
        var sender = GetPacketSender();

        if (player.TargetType == TargetType.Chest) {
            var chest = player.Target as IInstanceChest;

            if (chest is not null) {
                if (index > chest.Items.Count) {
                    return;
                }

                if (chest.OpenedByCharacterId > 0) {
                    if (chest.OpenedByCharacterId != player.Character.CharacterId) {
                        sender.SendMessage(SystemMessage.ChestIsOpenedByAnotherPlayer, QbColor.BrigthRed, player);

                        return;
                    }

                    index--;

                    var item = chest.Items[index];

                    bool canTakeItem;

                    if (item.IsCurrency) {
                        canTakeItem = TakeCurrency(sender, player, item);
                    }
                    else {
                        canTakeItem = TakeItem(sender, player, chest, item);
                    }

                    if (canTakeItem) {
                        chest.Items.RemoveAt(index);

                        sender.SendSortChestItemList(player, index);
                    }
                    else {
                        sender.SendEnableTakeItemFromChest(player);
                    }

                    var instance = GetInstance(player);

                    CheckForEmptyChest(sender, instance, chest);
                }
            }
        }
    }

    private void CheckForEmptyChest(IPacketSender sender, IInstance? instance, IInstanceChest chest) {
        if (chest.Items.Count == 0) {
            var id = chest.OpenedByCharacterId;

            chest.State = ChestState.Empty;

            if (id > 0) {
                var player = GetPlayerRepository().FindByCharacterId(id);

                if (player is not null) {
                    player.Target = null;
                    player.TargetType = TargetType.None;

                    sender.SendCloseChest(player, chest);
                    sender.SendTarget(player, TargetType.None, 0);
                }
            }

            if (instance is not null) {
                instance.Remove(chest);

                sender.SendUpdateChestState(instance, chest);
            }
        }
    }

    #endregion

    #region Take Item

    private bool TakeItem(IPacketSender sender, IPlayer player, IInstanceChest chest, IInstanceChestItem item) {
        if (item.CharacterIdFromRollDiceWinner > 0) {
            if (Environment.TickCount >= item.WinnerTimeLimit) {
                item.CharacterIdFromRollDiceWinner = 0;

                return false;
            }

            var winner = GetPlayerRepository().FindByCharacterId(item.CharacterIdFromRollDiceWinner);

            if (winner is not null) {
                return AddItem(sender, winner, item);
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
                return AddItem(sender, player, item);
            }
        }

        return false;
    }

    private bool AddItem(IPacketSender sender, IPlayer player, IInstanceChestItem item) {
        var empty = player!.Inventories.FindFreeInventory(player.Character.MaximumInventories);

        if (empty is not null) {
            empty.ItemId = item.Id;
            empty.Value = item.Value;
            empty.Level = item.Level;
            empty.Bound = item.Bound;
            empty.UpgradeId = item.UpgradeId;
            empty.AttributeId = item.AttributeId;

            sender.SendInventoryUpdate(player, empty.InventoryIndex);
            sender.SendMessage(SystemMessage.ReceivedItem, QbColor.BrigthCyan, player, new string[] { item.Id.ToString(), item.Value.ToString() });

            return true;
        }
        else {
            sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
        }

        return false;
    }

    #endregion

    #region Take Currency

    private bool TakeCurrency(IPacketSender sender, IPlayer player, IInstanceChestCurrency item) {
        var added = false;
        var rest = 0;

        if (player.PartyId > 0) {
            added = CanSharePartyCurrency(sender, player, item);

            if (!added) { 
                if (CanAddCurrency(player, item.Currency, item.Value, out rest)) {
                    added = true;

                    sender.SendCurrencyUpdate(player, item.Currency);
                    sender.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, player, new string[] { ((int)item.Currency).ToString(), item.Value.ToString() });
                }
            }
        }
        else {
            if (CanAddCurrency(player, item.Currency, item.Value, out rest)) {
                added = true;

                sender.SendCurrencyUpdate(player, item.Currency);
                sender.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, player, new string[] { ((int)item.Currency).ToString(), item.Value.ToString() });
            }
        }

        if (added && rest > 0) {
            item.Value = rest;

            sender.SendUpdateChestItem(player, (IInstanceChestItem)item);

            return false;
        }
        else if (added && rest < 1) {
            return true;
        }

        return false;
    }

    private bool CanSharePartyCurrency(IPacketSender sender, IPlayer player, IInstanceChestCurrency item) {
        var currentMapId = player.Character.Map;
        var party = GetParty(player);

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
                if (PartyShareCurrency(sender, player, item.Currency, item.Value, party, totalPlayers, out var rest)) {
                    var module = item.Value % totalPlayers;

                    if (module > 0) {
                        if (CanAddCurrency(player, item.Currency, module, out _)) {
                            sender.SendCurrencyUpdate(player, item.Currency);
                            sender.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, player, new string[] { ((int)item.Currency).ToString(), module.ToString() });
                        }                   
                    }

                    if (rest > 0) {
                        return PartyShareCurrency(sender, player, item.Currency, rest, party, totalPlayers, out _);
                    }

                    return true;
                }
            }
        }

        return false;
    }

    private bool PartyShareCurrency(IPacketSender sender, IPlayer player, CurrencyType currencyType, int currencyValue, Party party, int totalPlayers, out int restAdded) {
        var currency = currencyValue / totalPlayers;
        var currentMapId = player.Character.Map;
        var totalRest = 0;

        if (currency > 0) {
            var members = party.Members;

            foreach (var member in members) {
                if (member?.Player is not null) {
                    if (!member.Disconnected) {
                        if (currentMapId == member.Player.Character.Map) {
                            var partyPlayer = member.Player;

                            if (CanAddCurrency(partyPlayer, currencyType, currency, out var rest)) {
                                sender.SendCurrencyUpdate(partyPlayer, currencyType);
                                sender.SendMessage(SystemMessage.ReceivedCurrency, QbColor.Gold, partyPlayer, new string[] { ((int)currencyType).ToString(), (currency - rest).ToString() });
                            }
                            else {
                                sender.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, partyPlayer);
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

    private IInstance? GetInstance(IPlayer player) {
        var instances = InstanceService!.Instances;
        var instanceId = player.Character.Map;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private Party? GetParty(IPlayer player) {
        var parties = InstanceService!.Parties;
        var partyId = player.PartyId;

        parties.TryGetValue(partyId, out var party);

        return party;
    }

    private IDatabase<Drop> GetDatabaseDrop() {
        return ContentService!.Drops;
    }

    private IDatabase<Chest> GetDatabaseChest() {
        return ContentService!.Chests;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }
}