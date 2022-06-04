namespace Dragon.Network.Messaging.DTO;

public struct DataPartyMember {
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Model { get; set; }
    public bool Disconnected { get; set; }
    public int InstanceId { get; set; }
    public int[] Vital { get; set; } = Array.Empty<int>();
    public int[] MaximumVital { get; set; } = Array.Empty<int>();

    public DataPartyMember() {
        Index = 0;
        Model = 0;
        Disconnected = false;
        InstanceId = 0;
    }
}