namespace Dragon.Game.Configurations.Data;

public class Trade {
    public int Maximum { get; set; }
    public int AcceptTimeOut { get; set; }
    public int CheckIntegrityTimeOut { get; set; }
    public int MaximumTradeItems { get; set; }

    public Trade() {
        AcceptTimeOut = 15;
        CheckIntegrityTimeOut = 3;
        MaximumTradeItems = 15;
        Maximum = 1000;
    }
}