using Dragon.Core.Common;
using Dragon.Core.Services;

namespace Dragon.Login;

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