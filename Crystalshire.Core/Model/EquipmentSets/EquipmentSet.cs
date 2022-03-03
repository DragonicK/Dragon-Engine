namespace Crystalshire.Core.Model.EquipmentSets {
    public class EquipmentSet {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IDictionary<EquipmentSetCount, EquipmentSetEffect> Sets { get; set; }

        public EquipmentSet() {
            Name = string.Empty;
            Description = string.Empty;
            Sets = new Dictionary<EquipmentSetCount, EquipmentSetEffect>();
        }

        public override string ToString() {
            return Name;
        }
    }
}