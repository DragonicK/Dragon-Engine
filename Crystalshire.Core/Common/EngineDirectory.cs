namespace Crystalshire.Core.Common {
    public class EngineDirectory {
        private readonly List<string> directory;

        public EngineDirectory() {
            directory = new List<string>();
        }

        public void Add(string folder) {
            directory.Add(folder);
        }

        public void Create() {
            for (var i = 0; i < directory.Count; i++) {
                if (!Directory.Exists(directory[i])) {
                    Directory.CreateDirectory(directory[i]);
                }
            }
        }
    }
}
