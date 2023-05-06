using Dragon.Core.Services;

using Dragon.Network.Pool;

namespace Dragon.Game.Services;

public sealed class PoolService : IService {
    public ServicePriority Priority => ServicePriority.First;
    public IEngineBufferPool? EngineBufferPool { get; private set; }

    public void Start() {
        EngineBufferPool = new EngineBufferPool(short.MaxValue);
    }

    public void Stop() {

    }
}