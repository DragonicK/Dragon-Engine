namespace Crystalshire.Model.Models{
    public class Frame {
        public string Name { get; set; }
        public Bitmap? Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool CanMove { get; set; }
        public AttackType AttackType { get; set; }
        public int CastSkillId { get; set; }
        public Animation Animation { get; set; }

        public Frame() {
            Name = "No Name";
        }

        public Frame Clone() {
            var f = new Frame() {
                Name = Name,
                Width = Width,
                Height = Height,
                CanMove = CanMove,
                Animation = Animation,
                AttackType = AttackType,
                CastSkillId = CastSkillId
            };

            if (Image is not null) {
                f.Image = new Bitmap(Image);      
            }

            return f;
        }
    }
}