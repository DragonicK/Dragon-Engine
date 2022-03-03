namespace Crystalshire.Core.Model.Quests {
    public class QuestRequirement {
        public int NpcId { get; set; }
        public int ObjectId { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}