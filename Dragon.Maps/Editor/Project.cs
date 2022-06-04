using System.Drawing.Imaging;
using Dragon.Maps.Model;

namespace Dragon.Maps.Editor;

public class Project {
    public void Save(IMap map, string file) {
        var property = map.Property;
        var width = property.Width;
        var height = property.Height;

        using var f = new FileStream(file, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(f);

        writer.Write(property.Id);
        writer.Write(property.Name);
        writer.Write(property.Music);
        writer.Write(property.Ambience);

        writer.Write(property.Link.Up);
        writer.Write(property.Link.Down);
        writer.Write(property.Link.Left);
        writer.Write(property.Link.Right);

        writer.Write((int)property.Moral);
        writer.Write((int)property.Weather);

        writer.Write(property.Boot.Id);
        writer.Write(property.Boot.X);
        writer.Write(property.Boot.Y);

        writer.Write(property.Fog.Id);
        writer.Write((int)property.Fog.Blending);
        writer.Write(property.Fog.Opacity);
        writer.Write(property.Fog.Red);
        writer.Write(property.Fog.Green);
        writer.Write(property.Fog.Blue);

        writer.Write(property.Width);
        writer.Write(property.Height);

        writer.Write(property.KeyA);
        writer.Write(property.KeyB);
        writer.Write(property.KeyC);

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                Write(writer, map.Ground[x, y].Texture);
                Write(writer, map.Mask1[x, y].Texture);
                Write(writer, map.Mask2[x, y].Texture);
                Write(writer, map.Fringe1[x, y].Texture);
                Write(writer, map.Fringe2[x, y].Texture);
                Write(writer, map.Attribute[x, y]);
            }
        }
    }

    public IMap? Open(string file) {
        var map = new Map();
        var property = map.Property;

        using var f = new FileStream(file, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(f);

        property.Id = reader.ReadInt32();
        property.Name = reader.ReadString();
        property.Music = reader.ReadString();
        property.Ambience = reader.ReadString();

        var link = new Link {
            Up = reader.ReadInt32(),
            Down = reader.ReadInt32(),
            Left = reader.ReadInt32(),
            Right = reader.ReadInt32()
        };

        property.Link = link;

        property.Moral = (Moral)reader.ReadInt32();
        property.Weather = (Weather)reader.ReadInt32();

        var boot = new Boot {
            Id = reader.ReadInt32(),
            X = reader.ReadInt32(),
            Y = reader.ReadInt32()
        };

        property.Boot = boot;

        var fog = new Fog {
            Id = reader.ReadInt32(),
            Blending = (Blending)reader.ReadInt32(),
            Opacity = reader.ReadByte(),
            Red = reader.ReadByte(),
            Green = reader.ReadByte(),
            Blue = reader.ReadByte()
        };

        property.Fog = fog;

        var width = reader.ReadInt32();
        var height = reader.ReadInt32();

        property.Width = width;
        property.Height = height;

        property.KeyA = reader.ReadString();
        property.KeyB = reader.ReadString();
        property.KeyC = reader.ReadString();

        map.UpdateSize();

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                map.Ground[x, y].Texture = GetTexture(reader);
                map.Mask1[x, y].Texture = GetTexture(reader);
                map.Mask2[x, y].Texture = GetTexture(reader);
                map.Fringe1[x, y].Texture = GetTexture(reader);
                map.Fringe2[x, y].Texture = GetTexture(reader);
                map.Attribute[x, y] = GetAttributes(reader);
            }
        }

        return map;
    }

    private void Write(BinaryWriter writer, Tile attributes) {
        writer.Write(attributes.Data1);
        writer.Write(attributes.Data2);
        writer.Write(attributes.Data3);
        writer.Write(attributes.Data4);
        writer.Write(attributes.Data5);
        writer.Write(attributes.DirBlock);
        writer.Write((int)attributes.Type);
    }

    private void Write(BinaryWriter writer, Bitmap bitmap) {
        var buffer = GetBytes(bitmap);

        writer.Write(buffer.Length);
        writer.Write(buffer);
    }

    private Tile GetAttributes(BinaryReader reader) {
        return new Tile() {
            Data1 = reader.ReadInt32(),
            Data2 = reader.ReadInt32(),
            Data3 = reader.ReadInt32(),
            Data4 = reader.ReadInt32(),
            Data5 = reader.ReadInt32(),
            DirBlock = reader.ReadByte(),
            Type = (TileType)reader.ReadInt32()
        };
    }

    private byte[] GetBytes(Bitmap bitmap) {
        byte[] buffer;

        using (var ms = new MemoryStream()) {
            bitmap.Save(ms, ImageFormat.Png);
            buffer = ms.ToArray();
        }

        return buffer;
    }

    private Bitmap GetTexture(BinaryReader reader) {
        Bitmap bitmap;

        var length = reader.ReadInt32();

        using (var ms = new MemoryStream(reader.ReadBytes(length))) {
            bitmap = new Bitmap(ms);
        }

        return bitmap;
    }
}