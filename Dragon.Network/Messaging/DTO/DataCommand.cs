namespace Dragon.Network.Messaging.DTO;

public struct DataCommand {
    public string Command { get; set; }

    public DataCommand() {
        Command = string.Empty;
    }
}