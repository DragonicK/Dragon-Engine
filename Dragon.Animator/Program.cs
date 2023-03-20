using Dragon.Core.Common;

namespace Dragon.Animator {
    internal static class Program {
        public static JetBrainsMonoLoader? FontLoader { get; set; }
        public static JetBrainsMono? JetBrainsMono { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            FontLoader = new JetBrainsMonoLoader();

            FontLoader.LoadFromResource();

            JetBrainsMono = new JetBrainsMono(FontLoader);

            var directories = new EngineDirectory();

            directories.Add("./Content");
            directories.Add("./Resource");

            directories.Create();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.Run(new FormMain() {
                JetBrainsMono = JetBrainsMono
            });
        }
    }
}