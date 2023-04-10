using Dragon.Core.Model;
using Dragon.Core.Model.Chests;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Instances.Chests;

public class InstanceChest : IInstanceChest {
    public int Id { get; set; }
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Chest Chest { get; set; }
    public long OpenedByCharacterId { get; set; }
    public long CreateFromCharacterId { get; set; }
    public int PartyId { get; set; }
    public ChestState State { get; set; }
    public int RemainingTime { get; set; }
    public IInstance Instance { get; set; }
    public IList<IInstanceChestItem> Items { get; set; }
    public IEntityAttribute Attributes { get; set; }
    public IEntityCombat Combat { get; set; }
    public IEntityEffect Effects { get; set; }
    public IEntityVital Vitals { get; set; }
    public IEntity? Target { get; set; }
    public TargetType TargetType { get; set; }
    public int IndexOnInstance { get; set; }
    public bool IsDead { get; set; }

    public InstanceChest(IInstance instance) {
        Instance = instance;

        Items = new List<IInstanceChestItem>();
    }

    public int GetX() {
        throw new NotImplementedException();
    }

    public int GetY() {
        throw new NotImplementedException();
    }
}