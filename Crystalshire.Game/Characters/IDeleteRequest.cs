
namespace Crystalshire.Game.Characters {
    public interface IDeleteRequest {
        long Id { get; set; }
        string Name { get; set; }
        long AccountId { get; set; }
        long CharacterId { get; set; }
        DateTime? RequestDate { get; set; }
        DateTime? ExclusionDate { get; set; }
        int RemainingSeconds { get; set; }

        void Decrease();
        bool CanDelete();
    }
}