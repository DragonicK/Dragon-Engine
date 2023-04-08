using Dragon.Core.Common;
using Dragon.Core.Services;

namespace Dragon.Game;

public sealed class Starter {
    public ServiceBroker Services { get; set; }

    private readonly Thread thread;
    private bool running;

    public Starter() {
        Services = new ServiceBroker();
        thread = new Thread(Update);
    }

    public void Start() {
        CheckDirectory();

        Services.Start();

        thread.Start();
    }

    public void Stop() {
        Services.Stop();

        running = false;

        thread.Join(3000);
    }

    private void CheckDirectory() {
        var dir = new EngineDirectory();

        dir.Add("./Server");
        dir.Add("./Server/Logs");
        dir.Add("./Server/Maps");
        dir.Add("./Server/Shops");
        dir.Add("./Server/Fields");
        dir.Add("./Server/Chests");
        dir.Add("./Server/Gashas");
        dir.Add("./Server/Classes");
        dir.Add("./Server/Content");
        dir.Add("./Server/Upgrades");
        dir.Add("./Server/Premiums");
        dir.Add("./Server/Instances");
        dir.Add("./Server/Experiences");

        dir.Add("./Server/BlackMarket");
        dir.Add("./Server/BlackMarket/Pet");
        dir.Add("./Server/BlackMarket/Boost");
        dir.Add("./Server/BlackMarket/Supply");
        dir.Add("./Server/BlackMarket/Package");
        dir.Add("./Server/BlackMarket/Service");
        dir.Add("./Server/BlackMarket/Consumable");
        dir.Add("./Server/BlackMarket/Promotional");

        dir.Create();
    }

    private async void Update() {
        const int Delay = 999;

        var services = Services.GetContainer().GetUpdatableServices();

        running = true;

        while (running) {
            var count = services.Length;

            Parallel.For(0, count, index => services[index].Update(Delay));

            await Task.Delay(Delay);
        }
    }
}