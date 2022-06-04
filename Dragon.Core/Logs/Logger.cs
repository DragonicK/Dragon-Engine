using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Dragon.Core.Logs;

public class Logger : ILogger {
    public bool Enabled { get; set; }
    public bool Opened => isOpen;

    private readonly JsonSerializerOptions? options;

    private readonly string folder = string.Empty;
    private readonly string file = string.Empty;

    private StreamWriter? writer;
    private FileStream? stream;

    private bool isOpen;

    public Logger(string name, string folder, bool enabled) {
        var date = DateTime.Today;
        Enabled = enabled;

        if (Enabled) {
            this.folder = folder;
            file = $"{name} {date.Year}-{date.Month}-{date.Day}.txt";

            options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
        }
    }

    public string Open() {
        if (Enabled) {
            try {
                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }

                stream = new FileStream($"{folder}/{file}", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream) {
                    AutoFlush = true
                };
            }
            catch (Exception ex) {
                isOpen = false;
                return ex.Message;
            }

            isOpen = true;
        }

        return string.Empty;
    }

    public void Close() {
        if (Enabled) {
            writer?.Dispose();
            stream?.Dispose();
            isOpen = false;
        }
    }

    public void Write(Description description) {
        if (Enabled && isOpen) {
            if (writer is not null) {
                writer.Write(JsonSerializer.Serialize(description, options));
                writer.Write("\n");

                writer.Flush();
            }
        }
    }
}