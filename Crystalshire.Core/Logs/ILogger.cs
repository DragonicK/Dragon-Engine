namespace Crystalshire.Core.Logs;

public interface ILogger {
    public bool Enabled { get; set; }
    public bool Opened { get; }

    /// <summary>
    /// Opens the log and returns a message if there is an error.
    /// </summary>
    /// <returns></returns>
    string Open();

    /// <summary>
    /// Closes the file.
    /// </summary>
    void Close();

    /// <summary>
    /// Writes the description to file.
    /// </summary>
    /// <param name="description"></param>
    void Write(Description description);
}