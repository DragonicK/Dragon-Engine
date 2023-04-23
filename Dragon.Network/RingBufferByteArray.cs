namespace Dragon.Network;

public class RingBufferByteArray {
    private const int BufferSize = 256;
    private const int ListSize = 64;

    public long Sequence { get; set; }
    public int FromId { get; set; }
    public IConnection? Connection { get; set; }
    public byte[] ByteBuffer { get; private set; }
    public int Length { get; private set; }
    public TransmissionTarget TransmissionTarget { get; set; }
    public List<int> DestinationPeers { get; set; }
    public int ExceptDestination { get; set; }

    public RingBufferByteArray() {
        ByteBuffer = new byte[BufferSize];
        DestinationPeers = new List<int>(ListSize);
    }

    /// <summary>
    /// Add data to offset 4, first four bytes are pre-allocated to packet size.
    /// </summary>
    /// <param name="buffer"></param>
    public void SetOutgoingContent(byte[] buffer) {
        var length = buffer.Length + 4;

        if (length > BufferSize) {
            ByteBuffer = new byte[length];
        }

        Buffer.BlockCopy(buffer, 0, ByteBuffer, 4, buffer.Length);
        Length = length;
    }

    public void SetContent(byte[] buffer, int length) {
        if (length > BufferSize) {
            ByteBuffer = new byte[length];
        }

        Buffer.BlockCopy(buffer, 0, ByteBuffer, 0, length);
        Length = length;
    }

    public void GetContent(ref byte[] target, int offset) {
        if (Length > target.Length) {
            target = new byte[Length];
        }

        Buffer.BlockCopy(ByteBuffer, 0, target, offset, Length);
    }

    public void Reset() {
        Connection = null;
        DestinationPeers.Clear();

        Array.Clear(ByteBuffer, 0, ByteBuffer.Length);

        Length = 0;
    }
}