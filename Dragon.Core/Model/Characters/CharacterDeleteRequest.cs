namespace Dragon.Core.Model.Characters;

public class CharacterDeleteRequest {
    public long Id { get; set; }
    public string Name { get; set; }
    public long CharacterId { get; set; }
    public long AccountId { get; set; }
    public string MachineId { get; set; }
    public string IpAddress { get; set; }
    public DateTime? RequestDate { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public bool IsActive { get; set; }

    public CharacterDeleteRequest() {
        Name = string.Empty;
        MachineId = string.Empty;
        IpAddress = string.Empty;
    }
}