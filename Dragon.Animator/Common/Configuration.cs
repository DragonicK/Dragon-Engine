namespace Dragon.Animator.Common;

public class Configuration {
    public string ResourcePath { get; set; }
    public string OutputClientPath { get; set; }

    public Configuration() {
        ResourcePath = string.Empty;
        OutputClientPath = string.Empty;
    }
}