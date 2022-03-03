using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.Equipments;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.EquipmentSets;

namespace Crystalshire.Game.Players {
    public class PlayerEquipment : IPlayerEquipment {
        public IEntityAttribute Attributes { get; }
        public IDatabase<Item>? Items { get; set; }
        public IDatabase<Equipment>? Equipments { get; set; }
        public IDatabase<GroupAttribute>? EquipmentAttributes { get; set; }
        public IDatabase<GroupAttribute>? EquipmentUpgrades { get; set; }
        public IDatabase<EquipmentSet>? EquipmentSets { get; set; }
        public IDatabase<GroupAttribute>? EquipmentSetAttributes { get; set; }

        private readonly IList<CharacterEquipment> _equipments;
        private readonly Dictionary<int, EquipmentSetCount> _equipmentSets;
        private readonly long _characterId;

        public PlayerEquipment(long characterId, IList<CharacterEquipment> equipments) {
            _equipments = equipments;
            _characterId = characterId;

            _equipmentSets = new Dictionary<int, EquipmentSetCount>();

            Attributes = new EntityAttribute();
        }

        public CharacterEquipment? Get(PlayerEquipmentType index) {
            var selected = _equipments.FirstOrDefault(p => p.InventoryIndex == index);

            if (selected is null) {
                selected = new CharacterEquipment() {
                    InventoryIndex = index,
                    CharacterId = _characterId
                };

                _equipments.Add(selected);
            }

            return selected;
        }

        public bool IsEquipped(PlayerEquipmentType index) {
            var selected = _equipments.FirstOrDefault(p => p.InventoryIndex == index);

            if (selected is not null) {
                return selected.ItemId > 0;
            }

            return false;
        }

        public void Equip(PlayerEquipmentType index, CharacterInventory inventory) {
            var selected = _equipments.FirstOrDefault(p => p.InventoryIndex == index);

            if (selected is null) {
                selected = new CharacterEquipment();
                _equipments.Add(selected);
            }

            selected.Apply(inventory);

            selected.InventoryIndex = index;
            selected.CharacterId = _characterId;

            CheckForBindItem(selected);

            IncreaseEquipmentSet(selected);

            Attributes.Clear();

            UpdateEquipmentSetAttributes();
            UpdateEquipmentAttributes();
        }

        public void Unequip(PlayerEquipmentType index) {
            var selected = _equipments.FirstOrDefault(p => p.InventoryIndex == index);

            if (selected is not null) {
                DecreaseEquipmentSet(selected);

                selected.Clear();
         
                Attributes.Clear();

                UpdateEquipmentSetAttributes();
                UpdateEquipmentAttributes();
            }
        }

        public void UpdateAttributes() {
            Attributes.Clear();

            foreach (var equipment in _equipments) {
                IncreaseEquipmentSet(equipment);
            }

            UpdateEquipmentSetAttributes();
            UpdateEquipmentAttributes();
        }

        private void IncreaseEquipmentSet(CharacterEquipment inventory) {
            var item = Items![inventory.ItemId];
            var equipmentId = item!.EquipmentId;

            if (Equipments!.Contains(equipmentId)) {
                var equipment = Equipments[equipmentId]!;
                var id = equipment.EquipmentSetId;

                if (id > 0) {
                    IncreaseEquipmentSet(id);
                }
            }
        }

        private void DecreaseEquipmentSet(CharacterEquipment inventory) {
            var item = Items![inventory.ItemId];
            var equipmentId = item!.EquipmentId;

            if (Equipments!.Contains(equipmentId)) {
                var equipment = Equipments[equipmentId]!;
                var id = equipment.EquipmentSetId;

                if (id > 0) {
                    DecreaseEquipmentSet(id);
                }
            }
        }

        private void IncreaseEquipmentSet(int id) {
            if (_equipmentSets.ContainsKey(id)) {
                var value = _equipmentSets[id];
                _equipmentSets[id] = ++value;
            }
            else {
                _equipmentSets[id] = EquipmentSetCount.One;
            }
        }

        private void DecreaseEquipmentSet(int id) {
            if (_equipmentSets.ContainsKey(id)) {
                var value = _equipmentSets[id];

                if (value > EquipmentSetCount.None) {
                    _equipmentSets[id] = --value;

                    if (_equipmentSets[id] == EquipmentSetCount.None) {
                        _equipmentSets.Remove(id);
                    }
                }
            }
        }

        private void UpdateEquipmentSetAttributes() {
            foreach (var (id, count) in _equipmentSets) {
                if (EquipmentSets!.Contains(id)) {
                    var equipmentSet = EquipmentSets[id]!;

                    foreach (var (required, effect) in equipmentSet.Sets) {
                      
                        if (count >= required) {
                            var attributeId = effect.AttributeId;
                            
                            if (EquipmentSetAttributes!.Contains(attributeId)) {
                                var attribute = EquipmentSetAttributes[attributeId]!;

                                Attributes.Add(1, attribute, GroupAttribute.Empty);
                            }                 
                        }
                    }
                }
            }
        }

        private void UpdateEquipmentAttributes() {
            foreach (var equipment in _equipments) {
                if (equipment.ItemId > 0) {
                    var attributes = GetAttribute(equipment.AttributeId);

                    if (attributes is not null) {
                        var upgrade = GetUpgrade(equipment.UpgradeId);

                        Attributes.Add(equipment.Level, attributes, upgrade);
                    }
                }
            }
        }

        public IList<CharacterEquipment> ToList() {
            return _equipments;
        }

        private GroupAttribute? GetAttribute(int id) {
            if (EquipmentAttributes is not null) {
                if (EquipmentAttributes!.Contains(id)) {
                    return EquipmentAttributes[id];
                }
            }

            return null;
        }

        private GroupAttribute GetUpgrade(int id) {
            if (EquipmentUpgrades is not null) {
                if (EquipmentUpgrades.Contains(id)) {
                    return EquipmentUpgrades[id]!;
                }
            }

            return GroupAttribute.Empty;
        } 

        private void CheckForBindItem(CharacterEquipment equipment) {
            var item = Items![equipment.ItemId];

            if (item is not null) {
                if (item.Bind == BindType.Equipped) {
                    equipment.Bound = true;
                }
            }
        }
    }
}