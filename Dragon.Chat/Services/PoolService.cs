using Dragon.Core.Services;

using Dragon.Network.Pool;

using Dragon.Chat.Pool;

namespace Dragon.Chat.Services {
    public sealed class PoolService : IService {
        public ServicePriority Priority => ServicePriority.First;
        public ConfigurationService? Configuration { get; private set; }
        public BubblePool? BubblePool { get; private set; }  
        public TargetPool? TargetPool { get; private set; }
        public IEngineBufferPool? EngineBufferPool { get; private set; }

        public void Start() {
            var allocated = Configuration!.Allocation;

            var bubbleSize = allocated.BubblesAllocatedSize;
            var bubbleTextSize = allocated.BubbleTextAllocatedSize;

            BubblePool = new BubblePool(bubbleSize, bubbleTextSize);

            var outgoingSize = allocated.OutgoingMessageAllocatedSize;
            var incomingSize = allocated.IncomingMessageAllocatedSize;

            EngineBufferPool = new EngineBufferPool(incomingSize, outgoingSize);

            var targetSize = allocated.TargetsAllocatedSize;
            var targetListSize = allocated.TargetListAllocatedSize;

            TargetPool = new TargetPool(targetSize, targetListSize);
        }

        public void Stop() {
    
        }
    }
}