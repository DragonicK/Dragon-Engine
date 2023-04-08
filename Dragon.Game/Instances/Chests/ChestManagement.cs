using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Drops;
using Dragon.Core.Model.Chests;

using Dragon.Game.Players;
using Dragon.Game.Configurations;

namespace Dragon.Game.Instances.Chests;

public class ChestManagement {
    public IPlayer? Player { get; set; }
    public IDatabase<Drop>? Drops { get; set; }
    public IDatabase<Chest>? Chests { get; set; }
    public IConfiguration? Configuration { get; set; }

    private readonly Random random;

    public ChestManagement() {
        random = new Random();
    }

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
                    instanceChest.KilledByCharacterId = Player.Character.CharacterId;
                    instanceChest.RemainingTime = Configuration!.Drop.MonsterDuration;

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

            if (chest.MaximumDisplayedItems >= instanceChest.Items.Count) {
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
}