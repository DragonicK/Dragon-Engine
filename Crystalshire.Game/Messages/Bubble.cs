using Crystalshire.Core.Model;

namespace Crystalshire.Game.Messages {
    public sealed class Bubble {
        public int TargetIndex { get; set; }
        public TargetType TargetType { get; set; }
        public string Text { get; set; } = string.Empty;
        public QbColor Color { get; set; }
    }
}