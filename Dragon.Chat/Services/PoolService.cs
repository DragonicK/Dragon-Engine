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
            var bubbleCapacity = Configuration!.BubblePoolCapacity;
            var messageCapacity = Configuration!.MessagePoolCapacity;
            var maximumConnections = Configuration!.MaximumConnections;

            BubblePool = new BubblePool(bubbleCapacity);
            TargetPool = new TargetPool(messageCapacity, maximumConnections);
            EngineBufferPool = new EngineBufferPool(short.MaxValue);
        }

        public void Stop() {
    
        }
    }
}