namespace Crystalshire.Core.Model.Classes {
    public class ClassGrowth : IClassGrowth {
        public Dictionary<GrowthAttribute, float> Attributes { get; set; }
        
        public ClassGrowth() {
            Attributes = new Dictionary<GrowthAttribute, float>();
            var values = Enum.GetValues<GrowthAttribute>();
      
            foreach (var index in values) {
                Attributes[index] = 0;
            }
        }

        public float GetAttribute(int level, Dictionary<PrimaryAttribute, int> attributes) {
            var attribute = level * Attributes[GrowthAttribute.Level];

            for (var i = 1; i < Attributes.Count; ++i) {
                attribute += Attributes[(GrowthAttribute)i] * attributes[(PrimaryAttribute)i - 1];
            }

            return attribute;
        }
    }
}