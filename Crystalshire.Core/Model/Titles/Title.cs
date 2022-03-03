namespace Crystalshire.Core.Model.Titles {
    public class Title {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public Rarity Rarity { get; set; }
        public int AttributeId { get; set; }

        public Title() {
            Name = string.Empty;
            Description = string.Empty;
        }

        public override string ToString() {
            return Name;
        }
    }
}