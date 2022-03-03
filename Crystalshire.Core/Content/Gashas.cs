using Crystalshire.Core.Model.Gashas;
using Crystalshire.Core.Serialization;

namespace Crystalshire.Core.Content {
    public class Gashas : Database<Gasha> {

        private int ProcessedFiles = 0;

        public override void Load() {
            var files = Directory.GetFiles(Folder);

            if (files.Length > 0) {
                LoadGashas(files);
            }

            var folders = GetFolders(Folder);

            if (folders?.Length > 0) {
                foreach (var folder in folders) {
                    LoadGashas(GetFiles(folder));
                }
            }

            if (ProcessedFiles == 0) {
                SaveDefault();
            }
        }

        private void LoadGashas(string[]? files) {
            if (files is not null) {
                foreach (var file in files) {
                    if (Json.FileExists(file)) {
                        var gasha = Json.Get<Gasha>(file);

                        if (gasha is not null) {
                            if (gasha.Id != 0) {
                                Add(gasha.Id, gasha);
                                Json.Save(file, gasha);
                            }
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
            var gasha = new Gasha() {
                Id = 1
            };

            gasha.Items.Add(new GashaItem() {
                Id = 1,
                Value = 1
            });

            gasha.Items.Add(new GashaItem() {
                Id = 2,
                Value = 1
            });

            gasha.Items.Add(new GashaItem() {
                Id = 8,
                Value = 100
            });

            Json.Save($"{Folder}/default.json", gasha);
        }
    }
}