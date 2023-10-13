namespace Dragon.Network.Pool;

public sealed class EngineBufferPool : IEngineBufferPool {
    private int rIndex;
    private int wIndex;

    private readonly IList<IEngineBufferReader> readers;
    private readonly IList<IEngineBufferWriter> writers;

    private readonly object _rLock = new();
    private readonly object _wLock = new();

    public EngineBufferPool(int readerLength, int writerLength) {
        readers = new List<IEngineBufferReader>(readerLength);
        writers = new List<IEngineBufferWriter>(writerLength);

        for (var i = 0; i < readerLength; ++i) {
            readers.Add(new EngineBufferReader());
        }

        for (var i = 0; i < writerLength; ++i) {
            writers.Add(new EngineBufferWriter());
        }
    }

    public IEngineBufferReader GetNextBufferReader() {
        lock (_rLock) {
            rIndex = rIndex >= readers.Count ? 0 : rIndex++;

            return readers[rIndex];
        }
    }

    public IEngineBufferWriter GetNextBufferWriter() {
        lock (_wLock) {
            wIndex = wIndex >= writers.Count ? 0 : wIndex++;

            return writers[wIndex];
        }
    }
}