namespace Crystalshire.Maps;

internal static class Program {
    public static Starter? Starter { get; set; }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        Starter = new Starter();
        Starter.Start();

        Application.Run(new FormMain() {
            JetBrainsMono = Starter.JetBrainsMono!,
            Grid = Starter.Grid!,
            Tiles = Starter.Tiles!,
            Maps = Starter.Maps!,
            Colors = Starter.Colors!,
            TextureDirection = Starter.TextureDirection!,
            DirectionPosition = Starter.DirectionPosition!
        });
    }
}