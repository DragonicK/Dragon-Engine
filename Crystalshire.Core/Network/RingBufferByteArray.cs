namespace Crystalshire.Core.Network {
    public class RingBufferByteArray {
        private const int BufferSize = 512;
        private const int ListSize = 256;

        public int FromId { get; set; }
        public byte[] ByteBuffer { get; private set; }
        public int Length { get; private set; }
        public TransmissionTarget TransmissionTarget { get; set; }
        public List<int> DestinationPeers { get; set; }
        public int ExceptDestination { get; set; }

        public RingBufferByteArray() {
            ByteBuffer = new byte[BufferSize];
            DestinationPeers = new List<int>(ListSize);
        }

        public void SetContent(byte[] buffer) {
            if (buffer.Length > BufferSize) {
                ByteBuffer = new byte[buffer.Length];
            }

            Buffer.BlockCopy(buffer, 0, ByteBuffer, 0, buffer.Length);
            Length = buffer.Length;
        }

        public void GetContent(ref byte[] target) {
            if (Length > target.Length) {
                target = new byte[Length];
            }

            Buffer.BlockCopy(ByteBuffer, 0, target, 0, Length);
        }

        public void Reset() {
            DestinationPeers.Clear();

            Array.Clear(ByteBuffer, 0, ByteBuffer.Length);
            Length = 0;
        }
    }
}