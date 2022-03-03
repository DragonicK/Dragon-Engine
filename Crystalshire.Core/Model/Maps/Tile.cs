namespace Crystalshire.Core.Model.Maps {
    public sealed class Tile {
        public TileType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Data4 { get; set; }
        public int Data5 { get; set; }
        public byte DirBlock { get; set; }

        public Tile Clone() {
            return new Tile() {
                Type = Type,
                Data1 = Data1,
                Data2 = Data2,
                Data3 = Data3,
                Data4 = Data4,
                Data5 = Data5,
                DirBlock = DirBlock
            };
        }
    }
}