using Dragon.Core.Services;
using Dragon.Core.Model.Maps;
using Dragon.Core.Serialization;

namespace Dragon.Game.Services;

public sealed class PassphraseService : IService {
    public ServicePriority Priority => ServicePriority.First;
    public MapPassphrase? Passphrases { get; private set; }

    public void Start() {
        LoadPassphrases();
    }

    public void Stop() {
        Passphrases?.Clear();
    }

    private void LoadPassphrases() {
        const string File = "./Server/Passphrases.json";

        if (!Json.FileExists(File)) {
            var instance = new MapPassphrase();
            instance.Add(1, "be10ea8c4e5d464cc0e75621ceb360e4");
            instance.Add(2, "be10ea8c4e5d464cc0e75621ceb360e4");
            instance.Add(3, "be10ea8c4e5d464cc0e75621ceb360e4");

            Json.Save(File, instance);
        }
        else {
            Passphrases = Json.Get<MapPassphrase>(File);

            if (Passphrases is not null) {
                Json.Save(File, Passphrases);
            }
        }
    }
}