using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;

using Dragon.Game.Regions;

namespace Dragon.Game.Instances;

public sealed class InstanceEntityManager {
    public IDatabase<Npc>? Npcs { get; init; }
    public IDatabase<GroupAttribute>? NpcAttributes { get; init; }
    public IDatabase<Effect>? Effects { get; set; }
    public IDatabase<GroupAttribute>? EffectAttributes { get; set; }
    public IDatabase<GroupAttribute>? EffectUpgrades { get; set; }

    public void CreateEntities(IInstance instance) {
        if (Npcs is not null) {
            var entities = instance.RegionEntities;

            for (var i = 0; i < entities.Count; ++i) {
                var entity = entities[i];

                if (Npcs.Contains(entity.Id)) {
                    var npc = Npcs[entity.Id]!;

                    CreateEntity(instance, entity, npc);
                }
            }
        }
    }

    private void CreateEntity(IInstance instance, IRegionEntity regionEntity, Npc npc) {
        if (NpcAttributes is not null) {
            var entity = new InstanceEntity {
                Id = regionEntity.Id,
                X = regionEntity.X,
                Y = regionEntity.Y,
                MaximumRangeX = regionEntity.MaximumRangeX,
                MaximumRangeY = regionEntity.MaximumRangeY,
                IsFixed = regionEntity.IsFixed,
                Direction = regionEntity.Direction,
                Behaviour = npc.Behaviour,
            };

            entity.Effects.Effects = Effects;
            entity.Effects.EffectAttributes = EffectAttributes;
            entity.Effects.EffectUpgrades = EffectUpgrades;

            var attributeId = npc.AttributeId;

            if (NpcAttributes.Contains(attributeId)) {
                var attributes = NpcAttributes[attributeId]!;

                entity.Attributes.Add(1, attributes, GroupAttribute.Empty);

                entity.Vitals.SetMaximum(Vital.HP, entity.Attributes.Get(Vital.HP));
                entity.Vitals.SetMaximum(Vital.MP, entity.Attributes.Get(Vital.MP));
                entity.Vitals.SetMaximum(Vital.Special, entity.Attributes.Get(Vital.Special));
            }

            instance.Entities.Add(entity);

            var index = instance.Entities.Count - 1;

            entity.IndexOnInstance = index;
        }
    }
}