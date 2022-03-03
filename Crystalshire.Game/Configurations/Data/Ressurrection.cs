namespace Crystalshire.Game.Configurations.Data {
    public class Ressurrection {
        public int MaximumAcceptTimeOut { get; set; }
        public float ReduceExperience { get; set; }

        public Ressurrection() {
            MaximumAcceptTimeOut = 360;
        }
    }
}