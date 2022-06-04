using Dragon.Core.Serialization;
using Dragon.Core.Model.Classes;

namespace Dragon.Core.Content;

public class Classes : Database<IClass> {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadClasses(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadClasses(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadClasses(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var classe = Json.Get<IClass>(file);

                    if (classe is not null) {
                        Add(classe.Id, classe);
                        Json.Save(file, classe);
                    }

                    count++;
                }
            }
        }

        return count;
    }

    private void SaveDefault() {
        Json.Save($"{Folder}/default.json", new Class() { Id = 1 });
    }
}