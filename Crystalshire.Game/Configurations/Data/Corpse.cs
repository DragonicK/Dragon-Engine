namespace Crystalshire.Game.Configurations.Data {
    public class Corpse {
        public int MaximumItems { get; set; }
        public int ObjectDuration { get; set; }
        public int MonsterDuration { get; set; }
        public int BossDuration { get; set; }
        public int RollDiceDuration { get; set; }
        public int WinnerTimeLimit { get; set; }

        public Corpse() {
            MaximumItems = 20;
            ObjectDuration = 180;
            MonsterDuration = 180;
            BossDuration = 600;
            RollDiceDuration = 30;
            WinnerTimeLimit = 60;
        }
    }
}