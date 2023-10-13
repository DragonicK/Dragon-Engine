namespace Dragon.Network;

public sealed class PacketBuffer {
    private int rPosition = 0;
    private int wPosition = 0;

    private readonly byte[] buffer;

    public PacketBuffer(int capacity) {
        buffer = new byte[capacity];
    }

    public int Length() => wPosition - rPosition;

    public void Flush() {
        rPosition = 0;
        wPosition = 0;

        Array.Clear(buffer);
    }

    public void Trim() {
        if (rPosition >= wPosition) {
            Flush();
        }
    }

    public void Write(byte[] source, int length) {
        Buffer.BlockCopy(source, 0, buffer, wPosition, length);

        wPosition += length;
    }

    public int ReadInt32(bool advance = true) {
        var length = sizeof(int);
        var values = new byte[length];

        Buffer.BlockCopy(buffer, rPosition, values, 0, length);

        if (advance) {
            rPosition += length;
        }

        return BitConverter.ToInt32(values, 0);
    }

    public void ReadBytes(byte[] content, int length) {
        Buffer.BlockCopy(buffer, rPosition, content, 0, length);

        rPosition += length;
    }
}