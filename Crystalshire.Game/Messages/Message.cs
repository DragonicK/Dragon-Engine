using Crystalshire.Core.Model;

namespace Crystalshire.Game.Messages;

public sealed class Message {
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public ChatChannel Channel { get; set; }
    public AccountLevel AccountLevel { get; set; }
    public QbColor Color { get; set; }
}