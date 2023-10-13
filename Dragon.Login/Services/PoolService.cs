using Dragon.Core.Services;

using Dragon.Network.Pool;

namespace Dragon.Login.Services;

public sealed class PoolService : IService {
    public ServicePriority Priority => ServicePriority.First;
    public ConfigurationService? Configuration { get; private set; }
    public IEngineBufferPool? EngineBufferPool { get; private set; }

    public void Start() {
        var allocated = Configuration!.Allocation;
 
        var outgoingSize = allocated.OutgoingMessageAllocatedSize;
        var incomingSize = allocated.IncomingMessageAllocatedSize;

        EngineBufferPool = new EngineBufferPool(incomingSize, outgoingSize);
    }

    public void Stop() {

    }
}