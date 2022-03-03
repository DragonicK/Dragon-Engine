using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerInventory : IPlayerInventory {
        public IDatabase<Item>? Items { get; set; }
        public int Count => _inventories.Count;

        private readonly IList<CharacterInventory> _inventories;
        private long _characterId;

        private const int Invalid = -1;

        public PlayerInventory(long characterId, IList<CharacterInventory> inventories) {
            _inventories = inventories;
            _characterId = characterId;
        }

        public int Add(CharacterInventory source, int amount, int maximumInventories) {
            var inventory = FindByItemId(source.ItemId);

            if (inventory is not null) {
                inventory.Value += amount;
                source.Value -= amount;

                if (source.Value <= 0) {
                    source.Clear();
                }

                return inventory.InventoryIndex;
            }
            else {
                inventory = FindFreeInventory(maximumInventories);

                if (inventory is not null) {
                    inventory.ApplyFromAnotherCharacter(source);
                    inventory.Value = amount;
                    source.Value -= amount;

                    if (source.Value <= 0) {
                        source.Clear();
                    }

                    return inventory.InventoryIndex;
                }
            }

            return Invalid;
        }

        public int Add(CharacterInventory source, int maximumInventories) {
            var inventory = FindFreeInventory(maximumInventories);

            if (inventory is not null) {
                inventory.ApplyFromAnotherCharacter(source);
                source.Clear();

                return inventory.InventoryIndex;
            }

            return Invalid;
        }

        public int Add(CharacterWarehouse warehouse, int amount, int maximumInventories) {
            var inventory = FindByItemId(warehouse.ItemId);

            if (inventory is not null) {
                inventory.Value += amount;
                warehouse.Value -= amount;

                if (warehouse.Value <= 0) {
                    warehouse.Clear();
                }

                return inventory.InventoryIndex;
            }
            else {
                inventory = FindFreeInventory(maximumInventories);

                if (inventory is not null) {
                    inventory.Apply(warehouse);
                    inventory.Value = amount;
                    warehouse.Value -= amount;

                    if (warehouse.Value <= 0) {
                        warehouse.Clear();
                    }

                    return inventory.InventoryIndex;
                }
            }

            return Invalid;
        }

        public int Add(CharacterWarehouse warehouse, int maximumInventories) {
            var inventory = FindFreeInventory(maximumInventories);

            if (inventory is not null) {
                inventory.Apply(warehouse);
                warehouse.Clear();

                return inventory.InventoryIndex;
            }

            return Invalid;
        }

        public CharacterInventory? FindByItemId(int id) {
            return _inventories.FirstOrDefault(p => p.ItemId == id);
        }

        public CharacterInventory? FindByIndex(int index) {
            return _inventories.FirstOrDefault(p => p.InventoryIndex == index);
        }

        public IList<CharacterInventory> ToList() {
            return _inventories;
        }

        public void Swap(int source, int destination) {
            var _source = FindByIndex(source);
            var _destination = FindByIndex(destination);

            if (_source is not null) {
                if (_destination is null) {
                    _destination = new CharacterInventory() {
                        CharacterId = _characterId
                    };

                    _inventories.Add(_destination);
                }

                if (Items is not null) {
                    var item = Items[_source.ItemId]!;

                    if (item.MaximumStack > 0) {
                        if (_destination.ItemId == item.Id && _source.Level == _destination.Level) {
                            _destination.Value += _source.Value;
                            _source.Clear();
                        }
                        else {
                            _source.InventoryIndex = destination;
                            _destination.InventoryIndex = source;
                        }
                    }
                    else {
                        _source.InventoryIndex = destination;
                        _destination.InventoryIndex = source;
                    }
                }

            }
        }

        public CharacterInventory? FindFreeInventory(int maximum) {
            var count = _inventories.Count;
            var occupied = new HashSet<int>(count);
            var unoccupied = new HashSet<int>(count);

            CharacterInventory inventory;

            var ordered = _inventories.OrderBy(x => x.InventoryIndex).ToList();

            for (var i = 0; i < count; ++i) {
                inventory = ordered[i];

                if (inventory.ItemId > 0) {
                    occupied.Add(inventory.InventoryIndex);
                }
                else if (inventory.ItemId == 0) {
                    unoccupied.Add(inventory.InventoryIndex);
                }
            }

            for (var i = 1; i <= maximum; ++i) {
                if (unoccupied.Contains(i)) {
                    return FindByIndex(i);
                }

                if (!occupied.Contains(i)) {
                    inventory = new CharacterInventory() {
                        CharacterId = _characterId,
                        InventoryIndex = i
                    };

                    _inventories.Add(inventory);

                    return inventory;
                }
            }

            return null;
        }
    }
}