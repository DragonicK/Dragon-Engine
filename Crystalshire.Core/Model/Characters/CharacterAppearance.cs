namespace Crystalshire.Core.Model.Characters;

public class CharacterAppearance {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int FaceId { get; set; }
    public int HairId { get; set; }
    public int VoiceId { get; set; }
    public int SkinRgb { get; set; }
    public int HairRgb { get; set; }
    public int EyeRgb { get; set; }
    public int Height { get; set; }
}