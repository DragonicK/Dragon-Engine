using System.ComponentModel.DataAnnotations.Schema;

namespace Dragon.Core.Model.Characters;

public class Character {
    public long CharacterId { get; set; }
    public long AccountId { get; set; }
    public short CharacterIndex { get; set; }
    public string Name { get; set; }
    public short ClassCode { get; set; }
    public short Gender { get; set; }
    public int Model { get; set; }
    public int CostumeModel { get; set; }
    public int Level { get; set; }
    public byte SoulSickness { get; set; }
    public int Experience { get; set; }
    public int RecoveryExperience { get; set; }
    public int Fatigue { get; set; }
    public int FatigueRecovery { get; set; }
    public int Points { get; set; }
    public int Map { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public int GroupId { get; set; }
    public short MaximumInventories { get; set; }
    public short MaximumWarehouse { get; set; }
    public int TitleId { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public DateTime? LastLogoutDate { get; set; }
    public int BindMap { get; set; }
    public int BindX { get; set; }
    public int BindY { get; set; }
    public bool IsDeleted { get; set; }
    [NotMapped]
    public bool IsDead { get; set; }

    public Character() {
        Name = String.Empty;
    }
}