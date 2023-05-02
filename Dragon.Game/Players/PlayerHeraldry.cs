using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Attributes;

namespace Dragon.Game.Players;

public sealed class PlayerHeraldry : IPlayerHeraldry {
    public IEntityAttribute Attributes { get; }
    public IDatabase<Item>? Items { get; set; }
    public IDatabase<Heraldry>? Heraldries { get; set; }
    public IDatabase<GroupAttribute>? HeraldryAttributes { get; set; }
    public IDatabase<GroupAttribute>? HeraldryUpgrades { get; set; }

    public readonly IList<CharacterHeraldry> _heraldries;
    private readonly long _characterId;

    public PlayerHeraldry(long characterId, IList<CharacterHeraldry> heraldries) {
        _heraldries = heraldries;
        _characterId = characterId;
        Attributes = new EntityAttribute();
    }

    public CharacterHeraldry? Get(int index) {
        return _heraldries.FirstOrDefault(p => p.InventoryIndex == index);
    }

    public bool IsEquipped(int index) {
        var selected = _heraldries.FirstOrDefault(p => p.InventoryIndex == index);

        if (selected is not null) {
            return selected.ItemId > 0;
        }

        return false;
    }

    public void Equip(int index, CharacterInventory inventory) {
        var selected = _heraldries.FirstOrDefault(p => p.InventoryIndex == index);

        if (selected is null) {
            selected = new CharacterHeraldry();
            _heraldries.Add(selected);
        }

        selected.Apply(inventory);
        selected.InventoryIndex = index;
        selected.CharacterId = _characterId;

        CheckForBindItem(selected);

        Attributes.Clear();

        UpdateHeraldryAttributes();
    }

    public void Unequip(int index) {
        var selected = _heraldries.FirstOrDefault(p => p.InventoryIndex == index);

        if (selected is not null) {
            selected.Clear();

            Attributes.Clear();

            UpdateHeraldryAttributes();
        }
    }

    public void UpdateAttributes() {
        Attributes.Clear();

        UpdateHeraldryAttributes();
    }

    public IList<CharacterHeraldry> ToList() {
        return _heraldries;
    }

    public CharacterHeraldry? FindFreeHeraldry(int maximum) {
        var count = _heraldries.Count;
        var occupied = new HashSet<int>(count);
        var unnocupied = new Queue<int>(count);

        CharacterHeraldry heraldry;

        for (var i = 0; i < count; ++i) {
            heraldry = _heraldries[i];

            if (heraldry.ItemId > 0) {
                occupied.Add(heraldry.InventoryIndex);
            }
            else if (heraldry.ItemId == 0) {
                unnocupied.Enqueue(i);
            }
        }

        if (unnocupied.Count > 0) {
            return _heraldries[unnocupied.Dequeue()];
        }

        for (var i = 1; i <= maximum; ++i) {
            if (!occupied.Contains(i)) {
                heraldry = new CharacterHeraldry() {
                    CharacterId = _characterId,
                    InventoryIndex = i
                };

                _heraldries.Add(heraldry);

                return heraldry;
            }
        }

        return null;
    }

    private void UpdateHeraldryAttributes() {
        foreach (var heraldry in _heraldries) {
            if (heraldry.ItemId > 0) {
                var attributes = GetAttribute(heraldry.AttributeId);

                if (attributes is not null) {
                    var upgrade = GetUpgrade(heraldry.UpgradeId);

                    Attributes.Add(heraldry.Level, attributes, upgrade);
                }
            }
        }
    }

    private GroupAttribute? GetAttribute(int id) {
        if (HeraldryAttributes is not null) {
            if (HeraldryAttributes!.Contains(id)) {
                return HeraldryAttributes[id];
            }
        }

        return null;
    }

    private GroupAttribute GetUpgrade(int id) {
        if (HeraldryUpgrades is not null) {
            if (HeraldryUpgrades.Contains(id)) {
                return HeraldryUpgrades[id]!;
            }
        }

        return GroupAttribute.Empty;
    }

    private void CheckForBindItem(CharacterHeraldry equipment) {
        var item = Items![equipment.ItemId];

        if (item is not null) {
            if (item.Bind == BindType.Equipped) {
                equipment.Bound = true;
            }
        }
    }
}