using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.Classes;

namespace Crystalshire.Core.Content {
    public class Classes : Database<IClass> {

        private int ProcessedFiles = 0;

        public override void Load() {
            var files = Directory.GetFiles(Folder);

            if (files.Length > 0) {
                LoadClasses(files);
            }

            var folders = GetFolders(Folder);

            if (folders?.Length > 0) {
                foreach (var folder in folders) {
                    LoadClasses(GetFiles(folder));
                }
            }

            if (ProcessedFiles == 0) {
                SaveDefault();
            }
        }

        private void LoadClasses(string[]? files) {
            if (files is not null) {
                foreach (var file in files) {
                    if (Json.FileExists(file)) {
                        var classe = Json.Get<IClass>(file);

                        if (classe is not null) {
                            Add(classe.Id, classe);
                            Json.Save(file, classe);
                        }

                        ProcessedFiles++;
                    }
                }
            }
        }

        private string[]? GetFolders(string root) {
            return Directory.GetDirectories(root);
        }

        private string[]? GetFiles(string folder) {
            if (Directory.Exists(folder)) {
                return Directory.GetFiles(folder);
            }

            return null;
        }

        private void SaveDefault() {
            Json.Save($"{Folder}/default.json", new Class() {  Id = 1 });
        }
    }
}