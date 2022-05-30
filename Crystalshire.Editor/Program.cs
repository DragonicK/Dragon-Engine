using Crystalshire.Core.Common;

namespace Crystalshire.Editor;

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        var directories = new EngineDirectory();

        directories.Add("./Content");

        directories.Create();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new FormMain());
    }
}