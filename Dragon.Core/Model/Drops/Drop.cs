namespace Dragon.Core.Model.Drops;

public class Drop {
    public int NcpId { get; set; }
    public List<int> Chests { get; set; }

    public Drop() {
        Chests = new List<int>();
    }
}