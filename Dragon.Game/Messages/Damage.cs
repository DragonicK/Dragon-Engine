using Dragon.Core.Model;

namespace Dragon.Game.Messages;

public struct Damage {
    public ActionMessageType MessageType { get; set; }
    public ActionMessageFontType FontType { get; set; }
    public QbColor Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Message { get; set; } 
}