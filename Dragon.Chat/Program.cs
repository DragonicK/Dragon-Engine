namespace Dragon.Chat;

public class Program {
    static Starter? Server { get; set; }

    static void Main(string[] args) {
        Start();

        while (true) {
            var input = Console.ReadLine();

            Process(input);

            if (IsExit(input)) {
                break;
            }
        }

        Stop();
    }

    private static void Start() {
        ConsoleWrite("Starting Server");

        Server = new Starter();
        Server.Start();

        ConsoleWrite("Server Started");
    }

    private static void Stop() {
        Server?.Stop();

        ConsoleWrite("Server Stoped");
    }

    private static bool IsExit(string? input) {
        if (input is null) {
            return false;
        }

        return input == "exit";
    }

    private static void Process(string? input) {
        if (input is not null) {

        }
    }

    private static void ConsoleWrite(string message) {
        Console.Write("[");
        Console.Write(DateTime.Now.ToString("HH:mm:ss"));
        Console.Write("]");
        Console.Write($" [INFO] ");
        Console.Write(message);
        Console.Write(Environment.NewLine);
    }
}