namespace Dragon.Network.Pool;

public sealed class EngineBufferPool : IEngineBufferPool {
    private int index;

    private readonly IList<IEngineBuffer> buffers;

    private readonly object _lock = new();

    public EngineBufferPool(int capacity) {
        buffers = new List<IEngineBuffer>(capacity);

        for (var i = 0; i < capacity; ++i) {
            buffers.Add(new EngineBuffer());
        }
    }

    public IEngineBuffer GetNextBuffer() {
        lock (_lock) {
            index = index >= buffers.Count ? 0 : index;

            return buffers[index];
        }
    }
}