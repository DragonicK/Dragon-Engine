namespace Crystalshire.Model;

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

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        Application.Run(new FormMain() {
            JetBrainsMono = JetBrainsMono
        });
    }
}