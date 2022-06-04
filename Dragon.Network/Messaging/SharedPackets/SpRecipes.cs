namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpRecipes : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Recipes;
    public int[] Recipes { get; set; } = Array.Empty<int>();
}