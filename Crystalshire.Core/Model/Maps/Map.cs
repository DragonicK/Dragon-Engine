namespace Crystalshire.Core.Model.Maps {
    public class Map : IMap {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Music { get; set; }
        public string Ambience { get; set; }
        public int MaximumX { get; set; }
        public int MaximumY { get; set; }
        public Weather Weather { get; set; }
        public Moral Moral { get; set; }
        public Tile[,] Tile { get; set; } = new Tile[0, 0];

        public Map() {
            Name = string.Empty;
            Music = string.Empty;
            Ambience = string.Empty;
        }
    }
}