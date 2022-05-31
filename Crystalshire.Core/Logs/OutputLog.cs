namespace Crystalshire.Core.Logs;

public static class OutputLog {
    public static void Write(string message) {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} [INFO] " + message);
    }
}