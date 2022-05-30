namespace Crystalshire.Editor.Common;

public class Configuration {
    public string ServerPath { get; set; }
    public string OutputServerPath { get; set; }
    public string OutputClientPath { get; set; }

    public Configuration() {
        ServerPath = string.Empty;
        OutputServerPath = string.Empty;
        OutputClientPath = string.Empty;
    }
}