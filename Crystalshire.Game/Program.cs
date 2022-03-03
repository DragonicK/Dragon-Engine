using Crystalshire.Core.Logs;

namespace Crystalshire.Game {
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
            OutputLog.Write("Starting Server");

            Server = new Starter();
            Server.Start();

            OutputLog.Write("Server Started");
        }

        private static void Stop() {
            Server?.Stop();

            OutputLog.Write("Server Stoped");
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
    }
}