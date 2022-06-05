namespace Dragon.Core.Logs;

public interface ILogger {
    void Start();
    void Stop();
    void Info(string header, string message);
    void Debug(string header, string message);
    void Warning(string header, string message);
    void Error(string header, string message);
    void Write(WarningLevel level, string header, string message);
}