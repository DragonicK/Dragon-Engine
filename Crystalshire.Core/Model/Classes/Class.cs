namespace Crystalshire.Core.Model.Classes {
    public class Class : IClass {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selectable { get; set; }

        public int[] MaleSprites { get; set; }
        public int[] FemaleSprites { get; set; }
        public bool GenderLock { get; set; }
        public Gender Gender { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int AttributePoint { get; set; }
        public int AttributePointPerLevel { get; set; }

        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Dictionary<Vital, int> Vital { get; set; }
        public Dictionary<PrimaryAttribute, int> Primary { get; set; }
        public Dictionary<SecondaryAttribute, int> Secondary { get; set; }
        public Dictionary<UniqueAttribute, float> Unique { get; set; }
        public Dictionary<ElementAttribute, int> ElementAttack { get; set; }
        public Dictionary<ElementAttribute, int> ElementDefense { get; set; }

        public Dictionary<Vital, IClassGrowth> VitalGrowth { get; set; }
        public Dictionary<SecondaryAttribute, IClassGrowth> SecondaryGrowth { get; set; }
        public Dictionary<UniqueAttribute, IClassGrowth> UniqueGrowth { get; set; }

        public IList<ClassSkill> Skills { get; set; }
        public IList<ClassPassive> Passives { get; set; }
        public IList<ClassInventory> Inventories { get; set; }
        public IList<ClassEquipment> Equipments { get; set; }

        public Class() {
            Name = string.Empty;

            FemaleSprites = Array.Empty<int>();
            MaleSprites = Array.Empty<int>();

            Vital = new Dictionary<Vital, int>();
            Primary = new Dictionary<PrimaryAttribute, int>();
            Secondary = new Dictionary<SecondaryAttribute, int>();
            Unique = new Dictionary<UniqueAttribute, float>();
            ElementAttack = new Dictionary<ElementAttribute, int>();
            ElementDefense = new Dictionary<ElementAttribute, int>();

            VitalGrowth = new Dictionary<Vital, IClassGrowth>();
            SecondaryGrowth = new Dictionary<SecondaryAttribute, IClassGrowth>();
            UniqueGrowth = new Dictionary<UniqueAttribute, IClassGrowth>();

            var vitals = Enum.GetValues<Vital>();
            foreach (var index in vitals) {
                Vital[index] = 0;
                VitalGrowth[index] = new ClassGrowth();
            }

            var primary = Enum.GetValues<PrimaryAttribute>();
            foreach (var index in primary) {
                Primary[index] = 0;
            }

            var secondary = Enum.GetValues<SecondaryAttribute>();
            foreach (var index in secondary) {
                Secondary[index] = 0;
                SecondaryGrowth[index] = new ClassGrowth();
            }

            var unique = Enum.GetValues<UniqueAttribute>();
            foreach (var index in unique) {
                Unique[index] = 0;
                UniqueGrowth[index] = new ClassGrowth();
            }

            var element = Enum.GetValues<ElementAttribute>();
            foreach (var index in element) {
                ElementAttack[index] = 0;
                ElementDefense[index] = 0;
            }

            Skills = new List<ClassSkill>() {
                new ClassSkill()
            };

            Passives = new List<ClassPassive>() {
                new ClassPassive()
            };

            Inventories = new List<ClassInventory>() {
                new ClassInventory()
            };

            Equipments = new List<ClassEquipment>() {
                new ClassEquipment()
            };
        }
    }
}