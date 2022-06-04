using System.Drawing.Imaging;
using Dragon.Core.Cryptography;

using Dragon.Maps.Model;
using Dragon.Maps.Common;

namespace Dragon.Maps.Editor;

public class Export {

    const int SizeIndex = 4;
    const int VersionSavedBytes = 16;
    const int DateTimeSavedBytes = 20;
    const int PropertySavedBytes = 383;
    const int AttributesSavedBytes = 22;

    private IList<ExportData> listGround;
    private IList<ExportData> listFringe;

    public Export() {
        listGround = new List<ExportData>();
        listFringe = new List<ExportData>();
    }

    public void ExportToPng(IMap map, string path) {
        var images = GetImages(map);
        var file = path.Replace(".png", string.Empty);

        images[0].Save($"{file}_Ground.png", ImageFormat.Png);
        images[1].Save($"{file}_Mask1.png", ImageFormat.Png);
        images[2].Save($"{file}_Mask2.png", ImageFormat.Png);
        images[3].Save($"{file}_Fringe1.png", ImageFormat.Png);
        images[4].Save($"{file}_Fringe2.png", ImageFormat.Png);
    }

    public void ExportToEngine(IMap map, byte[] key, byte[] iv, string path) {
        var version = new Version() {
            Major = 1,
            Minor = 0,
            Build = 1,
            Revision = 0
        };

        CreateTileData(map, key, iv);

        ExportProperty(map, path.Replace(".Maps", ".dat"));

        using var f = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(f);

        WriteSavedBytes(writer, map.Property);
        Write(writer, version);
        Write(writer, DateTime.Now);
        Write(writer, map.Property);
        Write(writer, map.Property, map.Attribute);
        Write(writer, listGround);
        Write(writer, listFringe);
    }

    private Bitmap[] GetImages(IMap map) {
        var property = map.Property;

        var tileSize = Constants.TileSize;

        var width = property.Width;
        var height = property.Height;

        var images = new Bitmap[5];

        for (var i = 0; i < 5; ++i) {
            images[i] = new Bitmap(width * tileSize, height * tileSize);
        }

        Graphics g;

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                g = Graphics.FromImage(images[0]);
                g.DrawImage(map.Ground[x, y].Texture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));

                g = Graphics.FromImage(images[1]);
                g.DrawImage(map.Mask1[x, y].Texture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));

                g = Graphics.FromImage(images[2]);
                g.DrawImage(map.Mask2[x, y].Texture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));

                g = Graphics.FromImage(images[3]);
                g.DrawImage(map.Fringe1[x, y].Texture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));

                g = Graphics.FromImage(images[4]);
                g.DrawImage(map.Fringe2[x, y].Texture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
            }
        }

        return images;
    }

    private Bitmap[] GetGroundAndFringe(Bitmap[] images) {
        var width = images[0].Width;
        var height = images[0].Height;

        var ground = new Bitmap(width, height);
        var fringe = new Bitmap(width, height);

        var g = Graphics.FromImage(ground);

        g.DrawImage(images[0], new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
        g.DrawImage(images[1], new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
        g.DrawImage(images[2], new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

        g = Graphics.FromImage(fringe);

        g.DrawImage(images[3], new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
        g.DrawImage(images[4], new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

        return new Bitmap[] { ground, fringe };
    }

    private void CreateTileData(IMap map, byte[] key, byte[] iv) {
        var property = map.Property;
        var images = GetGroundAndFringe(GetImages(map));

        var parallaxSize = Constants.ParallaxSize;
        var tileSize = Constants.TileSize;

        var aes = new AES() {
            CipherMode = System.Security.Cryptography.CipherMode.CBC,
            PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7,
        };

        var xCount = (property.Width * tileSize) / parallaxSize;
        var yCount = (property.Height * tileSize) / parallaxSize;

        listGround = new List<ExportData>();
        listFringe = new List<ExportData>();

        var texture = new Bitmap(parallaxSize, parallaxSize);

        var index = 0;

        for (var x = 0; x < xCount; ++x) {
            for (var y = 0; y < yCount; ++y) {

                index++;

                var g = Graphics.FromImage(texture);
                g.Clear(Color.Transparent);

                var dest = new Rectangle(0, 0, parallaxSize, parallaxSize);
                var source = new Rectangle(x * parallaxSize, y * parallaxSize, parallaxSize, parallaxSize);

                g.DrawImage(images[0], dest, source, GraphicsUnit.Pixel);

                var data = new ExportData() {
                    Id = index,
                    X = x * parallaxSize,
                    Y = y * parallaxSize,
                    Data = aes.Encrypt(GetBytes(texture), key, iv)
                };

                data.Length = data.Data.Length;

                listGround.Add(data);

                g.Clear(Color.Transparent);

                g.DrawImage(images[1], dest, source, GraphicsUnit.Pixel);

                data = new ExportData() {
                    Id = index,
                    X = x * parallaxSize,
                    Y = y * parallaxSize,
                    Data = aes.Encrypt(GetBytes(texture), key, iv)
                };

                data.Length = data.Data.Length;

                listFringe.Add(data);
            }
        }
    }

    private byte[] GetBytes(Bitmap bitmap) {
        byte[] buffer;

        using (var ms = new MemoryStream()) {
            bitmap.Save(ms, ImageFormat.Png);
            buffer = ms.ToArray();
        }
        return buffer;
    }

    private void WriteSavedBytes(BinaryWriter writer, IProperty property) {
        int totalBytes = 0;

        // Write Starter Index Data
        var attributes = (property.Width * property.Height) * AttributesSavedBytes;

        totalBytes += attributes;
        totalBytes += SizeIndex;
        totalBytes += VersionSavedBytes;
        totalBytes += DateTimeSavedBytes;
        totalBytes += PropertySavedBytes;

        writer.Write(totalBytes);
    }

    private void Write(BinaryWriter writer, Version version) {
        writer.Write(version.Major);
        writer.Write(version.Minor);
        writer.Write(version.Build);
        writer.Write(version.Revision);
    }

    private void Write(BinaryWriter writer, DateTime date) {
        writer.Write(date.Day);
        writer.Write(date.Month);
        writer.Write(date.Year);
        writer.Write(date.Hour);
        writer.Write(date.Minute);
    }

    private void Write(BinaryWriter writer, ExportData data) {
        writer.Write(data.Id);
        writer.Write(data.X);
        writer.Write(data.Y);
        writer.Write(data.Length);
        writer.Write(data.Data!);
    }

    private void Write(BinaryWriter writer, IProperty property) {
        writer.Write(property.Id);
        writer.Write(ConvertTextToBytes(property.Name, Constants.MaximumNameLength));
        writer.Write(ConvertTextToBytes(property.Music, Constants.MaximumNameLength));
        writer.Write(ConvertTextToBytes(property.Ambience, Constants.MaximumNameLength));

        writer.Write((byte)property.Moral);
        writer.Write((byte)property.Weather);

        writer.Write(property.Fog.Id);
        writer.Write(property.Fog.Opacity);
        writer.Write((byte)property.Fog.Blending);
        writer.Write(property.Fog.Red);
        writer.Write(property.Fog.Green);
        writer.Write(property.Fog.Blue);

        writer.Write(property.Width);
        writer.Write(property.Height);
    }

    private void Write(BinaryWriter writer, IProperty property, Tile[,] attributes) {
        var width = property.Width;
        var height = property.Height;

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                Write(writer, attributes[x, y]);
            }
        }
    }

    private void Write(BinaryWriter writer, Tile attributes) {
        writer.Write((byte)attributes.Type);
        writer.Write(attributes.Data1);
        writer.Write(attributes.Data2);
        writer.Write(attributes.Data3);
        writer.Write(attributes.Data4);
        writer.Write(attributes.Data5);
        writer.Write(attributes.DirBlock);
    }

    private void Write(BinaryWriter writer, IList<ExportData> list) {
        writer.Write(list.Count);

        for (var i = 0; i < list.Count; i++) {
            Write(writer, list[i]);
        }
    }

    /// <summary>
    /// Adiciona espaços em branco para que o Visual Basic 6 possa ler um tamanho fixo.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private byte[] ConvertTextToBytes(string text, int length) {
        const byte ASCIISpaceCode = 32;

        var empty = new byte[length];

        // Adiciona os espaços em branco.
        for (var i = 0; i < length; i++) {
            empty[i] = ASCIISpaceCode;
        }

        for (var i = 0; i < text.Length; i++) {
            empty[i] = (byte)text[i];
        }

        return empty;
    }

    private void ExportProperty(IMap map, string fileName) {
        var property = map.Property;
        var attributes = map.Attribute;

        var width = property.Width;
        var height = property.Height;

        using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(fs);

        writer.Write(property.Id);
        writer.Write(property.Name);
        writer.Write(property.Music);
        writer.Write(property.Ambience);

        writer.Write((byte)property.Moral);
        writer.Write((byte)property.Weather);

        writer.Write(property.Width);
        writer.Write(property.Height);

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                writer.Write((byte)attributes[x, y].Type);
                writer.Write(attributes[x, y].Data1);
                writer.Write(attributes[x, y].Data2);
                writer.Write(attributes[x, y].Data3);
                writer.Write(attributes[x, y].Data4);
                writer.Write(attributes[x, y].Data5);
                writer.Write(attributes[x, y].DirBlock);
            }
        }

        writer.Close();
        fs.Close();
    }
}