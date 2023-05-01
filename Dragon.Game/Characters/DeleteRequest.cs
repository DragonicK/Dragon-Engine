namespace Dragon.Game.Characters;

public sealed class DeleteRequest : IDeleteRequest {
    public long Id { get; set; }
    public string Name { get; set; }
    public long AccountId { get; set; }
    public long CharacterId { get; set; }
    public DateTime? RequestDate { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public int RemainingSeconds { get; set; }

    public DeleteRequest() {
        Name = string.Empty;
    }

    public void Decrease() => RemainingSeconds--;
    public bool CanDelete() => RemainingSeconds <= 0;
}