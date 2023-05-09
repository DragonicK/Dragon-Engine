namespace Dragon.Core.Logs;

public sealed class Description {
    public string Level { get; set; } = string.Empty;
    public string Date { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public Description() {
        Date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
    }
}