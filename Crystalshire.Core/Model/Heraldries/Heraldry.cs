namespace Crystalshire.Core.Model.Heraldries {
    public class Heraldry {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UpgradeId { get; set; }
        public IList<HeraldryAttribute> Attributes { get; set; }

        public Heraldry() {
            Name = string.Empty;
            Description = string.Empty;
            Attributes = new List<HeraldryAttribute>();
        }

        public override string ToString() {
            return Name;
        }
    }
}