using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerEffect : IEntityEffect {
        public IEntityAttribute Attributes { get; }
        public IDatabase<Effect>? Effects { get; set; }
        public IDatabase<GroupAttribute>? EffectAttributes { get; set; }
        public IDatabase<GroupAttribute>? EffectUpgrades { get; set; }
        public int Count => _effects.Count;

        private readonly IList<CharacterAttributeEffect> _effects;
        private readonly long _characterId;
        
        public PlayerEffect(long characterId, IList<CharacterAttributeEffect> effects) {
            _effects = effects;
            _characterId = characterId;
            Attributes = new EntityAttribute();
        }

        public CharacterAttributeEffect Add(int id, int level, int duration, bool isAura = false) {
            var selected = _effects.FirstOrDefault(p => p.EffectId == id);

            if (selected is null) {
                selected = FindEmptyEffect();
            }

            selected.EffectId = id;
            selected.EffectLevel = level;
            selected.EffectDuration = duration;
            selected.IsAura = isAura;

            return selected;
        }

        public bool Contains(int id) {
            var selected = _effects.FirstOrDefault(p => p.EffectId == id);

            return selected is not null;
        }

        public void Remove(int id) {
            var selected = _effects.FirstOrDefault(p => p.EffectId == id);

            if (selected is not null) {
                _effects.Remove(selected);
            }
        }

        public void UpdateAttributes() {
            Attributes.Clear();

            if (Effects is not null) {
                foreach (var current in _effects) {
                    if (Effects.Contains(current.EffectId)) {
                        var effect = Effects[current.EffectId]!;
                        var attributes = GetAttribute(effect.AttributeId);

                        if (attributes is not null) {
                            var upgrade = GetUpgrade(effect.UpgradeId);

                            Attributes.Add(current.EffectLevel, attributes, upgrade);
                        }
                    }
                }
            }
        }

        public CharacterAttributeEffect? GetOverridable(Effect source) {
            if (Effects is not null) {
                if (source.Override != EffectOverride.None) {

                    foreach (var inventory in _effects) {
                        var id = inventory.EffectId;

                        if (Effects.Contains(id)) {
                            var effect = Effects[id]!;

                            if (effect.Override == source.Override) {
                                return inventory;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public IList<CharacterAttributeEffect> ToList() {
            return _effects;    
        }

        private GroupAttribute? GetAttribute(int id) {
            if (EffectAttributes is not null) {
                if (EffectAttributes!.Contains(id)) {
                    return EffectAttributes[id];
                }
            }

            return null;
        }

        public CharacterAttributeEffect? GetEffectById(int id) {
            if (Effects is not null) {
                foreach (var inventory in _effects) {
                    if (inventory.EffectId == id) {
                        return inventory;
                    }
                }
            }

            return null;
        }

        private GroupAttribute GetUpgrade(int id) {
            if (EffectUpgrades is not null) {
                if (EffectUpgrades.Contains(id)) {
                    return EffectUpgrades[id]!;
                }
            }

            return GroupAttribute.Empty;
        }

        private CharacterAttributeEffect FindEmptyEffect() {
            var selected = _effects.FirstOrDefault(p => p.EffectId == 0);

            if (selected is null) {
                selected = new CharacterAttributeEffect() { 
                   CharacterId = _characterId
                };

                _effects.Add(selected);
            }

            return selected;
        }
    }
}