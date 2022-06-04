using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerInventory {
    IDatabase<Item>? Items { get; set; }
    int Count { get; }
    IList<CharacterInventory> ToList();
    int Add(CharacterInventory source, int amount, int maximumInventories);
    int Add(CharacterInventory source, int maximumInventories);
    int Add(CharacterWarehouse warehouse, int amount, int maximumInventories);
    int Add(CharacterWarehouse warehouse, int maximumInventories);
    CharacterInventory? FindByItemId(int id);
    CharacterInventory? FindByIndex(int index);
    CharacterInventory? FindFreeInventory(int maximum);
    void Swap(int source, int destination);
}