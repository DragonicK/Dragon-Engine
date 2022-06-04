using System.Drawing;
using System.Text.Json.Serialization;

namespace Dragon.Core.Logs;

public class Description {
    public string Date { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Warning => WarningCode.ToString();

    [JsonIgnore]
    public Color Color => WarningCode switch {
        WarningCode.Error => Color.Coral,
        WarningCode.Normal => Color.Black,
        WarningCode.Warning => Color.Gold,
        WarningCode.Success => Color.LawnGreen,
        WarningCode.Unknown => Color.DarkViolet,
        _ => Color.Black
    };

    [JsonIgnore]
    public WarningCode WarningCode { get; set; }

    public Description() {
        Date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
    }
}