using Crystalshire.Core.Model.Maps;

namespace Crystalshire.Maps.Model;

public interface IProperty {
    string Name { get; set; }
    string Music { get; set; }
    string Ambience { get; set; }
    Link Link { get; set; }
    Moral Moral { get; set; }
    Weather Weather { get; set; }
    Boot Boot { get; set; }
    Fog Fog { get; set; }
    int Width { get; set; }
    int Height { get; set; }
    string KeyA { get; set; }
    string KeyB { get; set; }
    string KeyC { get; set; }

    string GetHashText();
    byte[] GetHash();
}