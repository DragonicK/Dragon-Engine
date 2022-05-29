namespace Crystalshire.Core.Content {
    public class Database<T> : IDatabase<T> {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public int Count {
            get {
                return values.Count;
            }
        }

        protected IDictionary<int, T> values;

        public Database() {
            Folder = string.Empty;
            FileName = string.Empty;
            values = new Dictionary<int, T>();
        }

        public T? this[int id] {
            get {
                if (values.ContainsKey(id)) {
                    return values[id];
                }

                return default;
            }
            set {
                values[id] = value!;
            }
        }

        public void Add(int id, T item) {
            values.Add(id, item);
        }

        public bool Contains(int id) {
            return values.ContainsKey(id);
        }

        public void Remove(int id) {
            values.Remove(id);
        }

        public void Clear() {
            values.Clear();
        }

        public virtual void Load() { }

        public virtual void Save() { }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator() {
            return values.GetEnumerator();
        }

        public string[]? GetFolders(string root) {
            return Directory.GetDirectories(root);
        }

        public string[]? GetFiles(string folder) {
            if (Directory.Exists(folder)) {
                return Directory.GetFiles(folder);
            }

            return null;
        }
    }
}