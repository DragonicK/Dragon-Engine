using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Network;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Manager {
    public class TradeManager {
        public int Id { get; set; }
        public IPacketSender? PacketSender { get; init; }
        public IDatabase<Item>? Items { get; init; }
        public int AcceptTimeOut { get; set; }
        public TradeState State { get; set; }
        public IPlayer Starter { get; set; }
        public IPlayer Invited { get; set; }
        public TradeState StarterState { get; set; }
        public TradeState InvitedState { get; set; }

        private int StarterCurrency;
        private int InvitedCurrency;

        // Copia do item no inventario.
        private readonly CharacterInventory[] StarterInventory;
        private readonly CharacterInventory[] InvitedInventory;

        // Indice de inventario selecionado.
        private readonly int[] StarterInventoryIndex;
        private readonly int[] InvitedInventoryIndex;

        private readonly int MaximumTradeItems;

        private const int Invalid = -1;

        public TradeManager(int id, int maximumTradeItems, IPlayer starter, IPlayer invited) {
            Id = id;
            Starter = starter;
            Invited = invited;

            MaximumTradeItems = maximumTradeItems;

            State = TradeState.Waiting;
            StarterState = TradeState.Waiting;
            InvitedState = TradeState.Waiting;

            StarterInventory = new CharacterInventory[MaximumTradeItems];
            InvitedInventory = new CharacterInventory[MaximumTradeItems];

            StarterInventoryIndex = new int[MaximumTradeItems];
            InvitedInventoryIndex = new int[MaximumTradeItems];

            for (var i = 0; i < MaximumTradeItems; ++i) {
                StarterInventory[i] = new CharacterInventory() {
                    InventoryIndex = i + 1
                };

                InvitedInventory[i] = new CharacterInventory() {
                    InventoryIndex = i + 1
                };
            }
        }

        public void Accept(IPlayer player) {
            if (player == Starter) {
                if (StarterState == TradeState.Accpeted) {
                    return;
                }

                if (StarterState != TradeState.Confirmed) {
                    return;
                }

                StarterState = TradeState.Accpeted;
            }

            if (player == Invited) {
                if (InvitedState == TradeState.Accpeted) {
                    return;
                }

                if (InvitedState != TradeState.Confirmed) {
                    return;
                }

                InvitedState = TradeState.Accpeted;
            }

            if (StarterState == TradeState.Accpeted && InvitedState == TradeState.Accpeted) {
                State = TradeState.ServerChecking;
                StarterState = TradeState.ServerChecking;
                InvitedState = TradeState.ServerChecking;
            }

            PacketSender!.SendMessage(SystemMessage.YouAcceptedTrade, QbColor.BrigthGreen, player);
            SendTradeState();
        }

        public void Confirm(IPlayer player) {
            if (player == Starter) {
                if (StarterState == TradeState.Confirmed) {
                    return;
                }

                StarterState = TradeState.Confirmed;
            }

            if (player == Invited) {
                if (InvitedState == TradeState.Confirmed) {
                    return;
                }

                InvitedState = TradeState.Confirmed;
            }

            if (StarterState == TradeState.Confirmed && InvitedState == TradeState.Confirmed) {
                State = TradeState.Confirmed;
            }

            PacketSender!.SendMessage(SystemMessage.YouConfirmedTrade, QbColor.BrigthGreen, player);
            SendTradeState();
        }

        public void Decline() {
            if (Starter is not null) {
                Starter.TradeId = 0;
                PacketSender!.SendMessage(SystemMessage.DeclinedTrade, QbColor.BrigthRed, Starter);
                PacketSender!.SendCloseTrade(Invited);
            }

            if (Invited is not null) {
                Invited.TradeId = 0;
                PacketSender!.SendMessage(SystemMessage.DeclinedTrade, QbColor.BrigthRed, Invited);
                PacketSender!.SendCloseTrade(Invited);
            }

            State = TradeState.Failed;
        }

        public void TradeCurrency(IPlayer player, int amount) {
            if (IsAlreadyConfirmed(player)) {
                return;
            }

            if (player.Currencies.Get(CurrencyType.Gold) < amount) {
                amount = player.Currencies.Get(CurrencyType.Gold);
            }

            if (player == Starter) {
                StarterCurrency = amount;
            }

            if (player == Invited) {
                InvitedCurrency = amount;
            }

            if (Starter is not null) {
                PacketSender!.SendTradeCurrency(Starter, StarterCurrency, InvitedCurrency);
            }

            if (Invited is not null) {
                PacketSender!.SendTradeCurrency(Invited, StarterCurrency, InvitedCurrency);
            }
        }

        public void TradeItem(IPlayer player, int index, int amount) {
            if (MaximumTradeItems <= 0) {
                return;
            }

            if (IsAlreadyConfirmed(player)) {
                return;
            }

            var inventory = player.Inventories.FindByIndex(index);

            if (inventory is not null) {
                if (Items!.Contains(inventory.ItemId)) {
                    if (inventory.Bound) {
                        PacketSender!.SendMessage(SystemMessage.ItemCannotBeTraded, QbColor.BrigthRed, player);
                    }
                    else {
                        var shouldAdd = true;
                        var item = Items[inventory.ItemId];

                        if (item!.MaximumStack > 0) {
                            if (amount > inventory.Value) {
                                amount = inventory.Value;
                            }

                            var updated = UpdateTradedAmount(player, amount, inventory);

                            if (updated is not null) {
                                shouldAdd = false;

                                SendUpdate(player, updated, index);
                            }
                        }
                        else {
                            amount = 1;
                            shouldAdd = true;
                        }

                        if (shouldAdd) {
                            AddTradedItem(player, amount, inventory);
                        }
                    }
                }       
            }
        }

        public void UntradeItem(IPlayer player, int index) {
            if (MaximumTradeItems <= 0) {
                return;
            }

            if (IsAlreadyConfirmed(player)) {
                return;
            }

            if (index >= 1 && index <= MaximumTradeItems) {
                index--;

                var indexes = GetTradedInventoryIndexes(player);
                var inventories = GetTradedInventories(player);

                indexes[index] = 0;
                inventories[index].Clear();

                SendUpdate(player, inventories[index], 0);
            }
        }

        public void UpdateFailed() {
            State = TradeState.Failed;

            if (Starter is not null) {
                Starter.TradeId = 0;
                PacketSender!.SendMessage(SystemMessage.TradeAcceptTimeOut, QbColor.BrigthRed, Starter);
            }

            if (Invited is not null) {
                Invited.TradeId = 0;
                PacketSender!.SendMessage(SystemMessage.TradeAcceptTimeOut, QbColor.BrigthRed, Invited);
                PacketSender!.SendCloseTrade(Invited);
            }
        }

        public bool CanConclude() {
            if (State != TradeState.ServerChecking) {
                return false;
            }
           
            return true;
        }

        public void ExecuteTrade() {
            if (HasEnoughFreeInventory()) {
                if (CanTradeCurrency()) {
                    TradeItems();

                    PacketSender!.SendMessage(SystemMessage.TradeConcluded, QbColor.BrigthGreen, Starter);
                    PacketSender!.SendMessage(SystemMessage.TradeConcluded, QbColor.BrigthGreen, Invited);
                }
            }

            if (Starter is not null) {
                PacketSender!.SendCurrencyUpdate(Starter, CurrencyType.Gold);
            }

            if (Invited is not null) {
                PacketSender!.SendCurrencyUpdate(Invited, CurrencyType.Gold);
            }

            State = TradeState.Concluded;

            Destroy();
        }

        private void TradeItems() {
            if (Starter is not null && Invited is not null) {
                int starter_maximum = Starter.Character.MaximumInventories;
                int invited_maximum = Invited.Character.MaximumInventories;

                RemoveFromInventory(Starter);
                RemoveFromInventory(Invited);

                for (var i = 0; i < MaximumTradeItems; ++i) {
                    if (StarterInventoryIndex[i] > 0) {
                        var index = Invited.Inventories.Add(StarterInventory[i], invited_maximum);
    
                        if (index != Invalid) {
                            PacketSender!.SendInventoryUpdate(Invited, index);
                        }

                        PacketSender!.SendInventoryUpdate(Starter, StarterInventoryIndex[i]);
                    }

                    if (InvitedInventoryIndex[i] > 0) {
                        var index = Starter.Inventories.Add(InvitedInventory[i], starter_maximum);

                        if (index != Invalid) {
                            PacketSender!.SendInventoryUpdate(Starter, index);
                        }

                        PacketSender!.SendInventoryUpdate(Invited, InvitedInventoryIndex[i]);
                    }
                }
            }  
        }

        private void RemoveFromInventory(IPlayer player) {
            var indexes = GetTradedInventoryIndexes(player);
            var inventories = GetTradedInventories(player);

            for (var i = 0; i < MaximumTradeItems; ++i) {
                if (indexes[i] > 0) {
                    var index = indexes[i];
                    var amount = inventories[i].Value;

                    var inventory = player.Inventories.FindByIndex(index);

                    if (inventory is not null) {
                        inventory.Value -= amount;

                        if (inventory.Value <= 0) {
                            inventory.Clear();
                        }
                    }
                }
            }
        }

        private void SendUpdate(IPlayer player, CharacterInventory traded, int index) {
            if (player == Starter) {
                if (Starter is not null) {
                    PacketSender!.SendTradeMyInventory(Starter, traded.InventoryIndex, index, traded.Value);
                    PacketSender!.SendTradeOtherInventory(Invited, traded);
                }
            }
            else {
                if (Invited is not null) {
                    PacketSender!.SendTradeMyInventory(Invited, traded.InventoryIndex, index, traded.Value);
                    PacketSender!.SendTradeOtherInventory(Starter, traded);
                }
            }
        }

        private CharacterInventory? UpdateTradedAmount(IPlayer player, int amount, CharacterInventory inventory) {
            var traded = FindTradedItem(GetTradedInventories(player), inventory.ItemId);

            // If exists, changes the amount.
            if (traded is not null) {
                traded.Value = amount;

                return traded;
            }

            return null;
        }

        private void AddTradedItem(IPlayer player, int amount, CharacterInventory inventory) {
            var traded = FindFreeTradedInventory(GetTradedInventories(player));

            if (traded is not null) {
                traded.ItemId = inventory.ItemId;
                traded.Value = amount;
                traded.Level = inventory.Level;
                traded.Bound = inventory.Bound;
                traded.AttributeId = inventory.AttributeId;
                traded.UpgradeId = inventory.UpgradeId;

                var indexes = GetTradedInventoryIndexes(player);

                indexes[traded.InventoryIndex - 1] = inventory.InventoryIndex;

                SendUpdate(player, traded, inventory.InventoryIndex);
            }
        }
        
        private CharacterInventory? FindTradedItem(CharacterInventory[] inventories, int itemId) {
            for (var i = 0; i < MaximumTradeItems; ++i) {
                if (inventories[i].ItemId == itemId) {
                    return inventories[i];
                }
            }

            return null;
        }

        private CharacterInventory? FindFreeTradedInventory(CharacterInventory[] inventories) {
            for (var i = 0; i < MaximumTradeItems; ++i) {
                if (inventories[i].ItemId == 0) {
                    return inventories[i];
                }
            }

            return null;
        }

        private CharacterInventory[] GetTradedInventories(IPlayer from) {
            if (from == Starter) {
                return StarterInventory;
            }

            return InvitedInventory;
        }

        private int[] GetTradedInventoryIndexes(IPlayer from) {
            if (from == Starter) {
                return StarterInventoryIndex;
            }

            return InvitedInventoryIndex;
        }

        private void SendTradeState() {
            if (Starter is not null) {
                PacketSender!.SendTradeState(Starter, State, StarterState, InvitedState);
            }

            if (Invited is not null) {
                PacketSender!.SendTradeState(Invited, State, StarterState, InvitedState);
            } 
        }

        private bool IsAlreadyConfirmed(IPlayer player) {
            var isConfirmed = false;

            if (player == Starter) {
                if (StarterState == TradeState.Accpeted || StarterState == TradeState.Confirmed) {
                    isConfirmed = true;
                }
            }

            if (player == Invited) {
                if (InvitedState == TradeState.Accpeted || InvitedState == TradeState.Confirmed) {
                    isConfirmed = true;
                }
            }

            if (isConfirmed) {
                PacketSender!.SendMessage(SystemMessage.YouCantChangeItemWhenConfirmed, QbColor.BrigthRed, player);
            }

            return isConfirmed;
        }

        private bool HasEnoughFreeInventory() {
            var starter_count = 0;
            var invited_count = 0;

            for (var i = 0; i < MaximumTradeItems; ++i) {
                if (StarterInventoryIndex[i] > 0) {
                    starter_count++;
                }

                if (InvitedInventoryIndex[i] > 0) {
                    invited_count++;
                }
            }

            if (Starter is not null) {
                if (GetTotalFreeInventory(Starter) < starter_count) {
                    var parameters = new string[1] { Starter.Character.Name };

                    if (Invited is not null) {
                        PacketSender!.SendMessage(SystemMessage.ThePlayerInventoryIsFull, QbColor.BrigthRed, Invited, parameters);
                    }

                    PacketSender!.SendMessage(SystemMessage.ThePlayerInventoryIsFull, QbColor.BrigthRed, Starter, parameters);

                    return false;
                }
            }

            if (Invited is not null) {
                if (GetTotalFreeInventory(Invited) < invited_count) {
                    var parameters = new string[1] { Invited.Character.Name };

                    if (Starter is not null) {
                        PacketSender!.SendMessage(SystemMessage.ThePlayerInventoryIsFull, QbColor.BrigthRed, Starter, parameters);
                    }
               
                    PacketSender!.SendMessage(SystemMessage.ThePlayerInventoryIsFull, QbColor.BrigthRed, Invited, parameters);

                    return false;
                }
            }

            return true;
        }

        private bool CanTradeCurrency() {
            if (Starter is not null && Invited is not null) {
                bool result;

                result = Starter.Currencies.Subtract(CurrencyType.Gold, StarterCurrency);
                
                if (!result) {
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeSubtracted, QbColor.BrigthRed, Starter);
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeSubtracted, QbColor.BrigthRed, Invited);

                    return false;
                }

                result = Invited.Currencies.Subtract(CurrencyType.Gold, InvitedCurrency);

                if (!result) {
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeSubtracted, QbColor.BrigthRed, Starter);
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeSubtracted, QbColor.BrigthRed, Invited);

                    return false;
                }

                if (!Starter.Currencies.Add(CurrencyType.Gold, InvitedCurrency)) {
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, Starter);
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, Invited);

                    // Give gold back to inventory.
                    Starter.Currencies.Round(CurrencyType.Gold, StarterCurrency);
                    Invited.Currencies.Round(CurrencyType.Gold, InvitedCurrency);

                    return false;
                }

                if (!Invited.Currencies.Add(CurrencyType.Gold, StarterCurrency)) {
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, Starter);
                    PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthRed, Invited);

                    // Give gold back to inventory.
                    Starter.Currencies.Round(CurrencyType.Gold, StarterCurrency);
                    Invited.Currencies.Round(CurrencyType.Gold, InvitedCurrency);

                    return false;
                }

                return true;
            }

            return false;
        }

        private int GetTotalFreeInventory(IPlayer player) {
            var inventories = player.Inventories.ToList();
            var count = player.Character.MaximumInventories;

            for (var i = 0; i < inventories.Count; ++i) {
                if (inventories[i].ItemId > 0) {
                    count--;
                }
            }

            return count;       
        }

        private void Destroy() {
            if (Starter is not null) {
                Starter.TradeId = 0;
                PacketSender!.SendCloseTrade(Starter);
            }

            if (Invited is not null) {
                Invited.TradeId = 0;
                PacketSender!.SendCloseTrade(Invited);
            }
        }
    }
}