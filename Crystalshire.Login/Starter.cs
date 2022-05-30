using Crystalshire.Core.Common;
using Crystalshire.Core.Services;

namespace Crystalshire.Login;

public sealed class Starter {
    public ServiceBroker Services { get; set; }

    public Starter() {
        Services = new ServiceBroker();
    }

    public void Start() {
        CheckDirectory();

        Services.Start();
    }

    public void Stop() {
        Services.Stop();
    }

    private static void CheckDirectory() {
        var dir = new EngineDirectory();

        dir.Add("./Server");
        dir.Add("./Server/Logs");

        dir.Create();
    }
}