namespace Dragon.Game.Configurations.Data;

public class ChestDrop {
    public int MonsterDuration { get; set; }
    public int BossDuration { get; set; }
    public int RollDiceDuration { get; set; }
    public int WinnerTimeLimit { get; set; }

    public ChestDrop() {
        MonsterDuration = 180;
        BossDuration = 600;
        RollDiceDuration = 30;
        WinnerTimeLimit = 60;
    }
}