using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public class PlayerWarehouse : IPlayerWarehouse {
    public IDatabase<Item>? Items { get; set; }
    public int Count => _warehouse.Count;

    private readonly IList<CharacterWarehouse> _warehouse;
    private readonly long _characterId;

    private const int Invalid = -1;

    public PlayerWarehouse(long character, IList<CharacterWarehouse> warehouse) {
        _warehouse = warehouse;
        _characterId = character;
    }

    public int Deposit(CharacterInventory inventory, int amount, int maximumWarehouse) {
        var warehouse = FindByItemId(inventory.ItemId);

        if (warehouse is not null) {
            warehouse.Value += amount;
            inventory.Value -= amount;

            if (inventory.Value <= 0) {
                inventory.Clear();
            }

            return warehouse.InventoryIndex;
        }
        else {
            warehouse = FindFreeInventory(maximumWarehouse);

            if (warehouse is not null) {
                warehouse.Apply(inventory);
                warehouse.Value = amount;
                inventory.Value -= amount;

                if (inventory.Value <= 0) {
                    inventory.Clear();
                }

                return warehouse.InventoryIndex;
            }
        }

        return Invalid;
    }

    public int Deposit(CharacterInventory inventory, int maximumWarehouse) {
        var warehouse = FindFreeInventory(maximumWarehouse);

        if (warehouse is not null) {
            warehouse.Apply(inventory);
            inventory.Clear();

            return warehouse.InventoryIndex;
        }

        return Invalid;
    }

    public CharacterWarehouse? FindByItemId(int id) {
        return _warehouse.FirstOrDefault(p => p.ItemId == id);
    }

    public void RemoveByIndex(int index) {

    }

    public CharacterWarehouse? FindByIndex(int index) {
        return _warehouse.FirstOrDefault(p => p.InventoryIndex == index);
    }

    public IList<CharacterWarehouse> ToList() {
        return _warehouse;
    }

    public void Swap(int source, int destination) {
        var _source = FindByIndex(source);
        var _destination = FindByIndex(destination);

        if (_source is not null) {

            if (_destination is null) {
                _destination = new CharacterWarehouse() {
                    CharacterId = _characterId
                };

                _warehouse.Add(_destination);
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

    public CharacterWarehouse? FindFreeInventory(int maximum) {
        var count = _warehouse.Count;
        var occupied = new HashSet<int>(count);
        var unnocupied = new Queue<int>(count);

        CharacterWarehouse inventory;

        for (var i = 0; i < count; ++i) {
            inventory = _warehouse[i];

            if (inventory.ItemId > 0) {
                occupied.Add(inventory.InventoryIndex);
            }
            else if (inventory.ItemId == 0) {
                unnocupied.Enqueue(i);
            }
        }

        if (unnocupied.Count > 0) {
            return _warehouse[unnocupied.Dequeue()];
        }

        for (var i = 1; i <= maximum; ++i) {
            if (!occupied.Contains(i)) {
                inventory = new CharacterWarehouse() {
                    CharacterId = _characterId,
                    InventoryIndex = i
                };

                _warehouse.Add(inventory);

                return inventory;
            }
        }

        return null;
    }
}