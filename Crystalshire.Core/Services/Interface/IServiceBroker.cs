namespace Crystalshire.Core.Services {
    public interface IServiceBroker {
        IServiceContainer GetContainer();
        void Start();
        void Stop();
    }
}