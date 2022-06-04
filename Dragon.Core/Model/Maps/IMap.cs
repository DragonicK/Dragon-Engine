namespace Dragon.Core.Model.Maps;

public interface IMap {
    int Id { get; set; }
    string Name { get; set; }
    string Music { get; set; }
    string Ambience { get; set; }

    int MaximumX { get; set; }
    int MaximumY { get; set; }

    Weather Weather { get; set; }
    Moral Moral { get; set; }
    Tile[,] Tile { get; set; }
}