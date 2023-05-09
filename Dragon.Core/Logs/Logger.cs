using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Collections.Concurrent;

namespace Dragon.Core.Logs;

public sealed class Logger : ILogger {
    readonly JsonSerializerOptions? options;
    readonly ConcurrentQueue<(WarningLevel, Description)> queue;
    readonly Thread thread;
    readonly SemaphoreSlim semaphore;
    readonly StreamWriter writer;

    bool running;

    public Logger(string path) {
        options = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        queue = new ConcurrentQueue<(WarningLevel, Description)>();
        thread = new Thread(Loop);
        semaphore = new SemaphoreSlim(0);

        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        var fileName = $"{DateTime.Now:yyyy-MM-dd}.txt";

        writer = File.AppendText(Path.Combine(path, fileName));
        writer.AutoFlush = true;
    }

    public void Start() {
        running = true;
        thread.Start();
    }

    public void Stop() {
        running = false;
        semaphore.Release();
        thread.Join();
    }

    public void Info(string header, string message) => Write(WarningLevel.Info, header, message);

    public void Debug(string header, string message) => Write(WarningLevel.Debug, header, message);

    public void Warning(string header, string message) => Write(WarningLevel.Warning, header, message);

    public void Error(string header, string message) => Write(WarningLevel.Error, header, message);

    public void Write(WarningLevel level, string header, string message) {
        var desc = new Description() {
            Level = GetWarningLevelName(level),
            Message = message,
            Name = header
        };

        queue.Enqueue((level, desc));

        semaphore.Release();
    }

    private void Serialize(Description description) {
        writer.Write(JsonSerializer.Serialize(description, options));
        writer.Write(",");
        writer.Write(Environment.NewLine);
    }

    private void Loop() {
        while (running) {
            semaphore.Wait();

            while (!queue.IsEmpty) {
                if (!queue.TryDequeue(out (WarningLevel Level, Description Description) item)) {
                    continue;
                }

                var level = item.Level;
                var header = item.Description.Name;
                var message = item.Description.Message;

                Serialize(item.Description);

                ConsoleWrite(level, $"{header}: {message}");
            }
        }
    }

    private void ConsoleWrite(WarningLevel level, string message) {
        var color = GetColor(level);
        var name = GetWarningLevelName(level);
        var lastColor = Console.ForegroundColor;

        Console.Write("[");
        Console.Write(DateTime.Now.ToString("HH:mm:ss"));
        Console.Write("]");
        Console.ForegroundColor = color;
        Console.Write($" {name} ");
        Console.ForegroundColor = lastColor;
        Console.Write(message);
        Console.Write(Environment.NewLine);
    }

    private ConsoleColor GetColor(WarningLevel level) => level switch {
        WarningLevel.Info => ConsoleColor.White,
        WarningLevel.Error => ConsoleColor.Red,
        WarningLevel.Warning => ConsoleColor.Yellow,
        WarningLevel.Debug => ConsoleColor.Blue,
        _ => ConsoleColor.White
    };

    private string GetWarningLevelName(WarningLevel level) => level switch {
        WarningLevel.Info => "[INFO]",
        WarningLevel.Error => "[ERROR]",
        WarningLevel.Warning => "[WARNING]",
        WarningLevel.Debug => "[DEBUG]",
        _ => "[UNKNOWN]"
    };
}