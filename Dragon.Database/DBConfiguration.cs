using System.Text;

namespace Dragon.Database;

public class DBConfiguration {
    public string DataSource { get; set; }
    public int Port { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }
    public int MinPoolSize { get; set; }
    public int MaxPoolSize { get; set; }

    private string? connection = null;

    public DBConfiguration() {
        DataSource = "0.0.0.0";
        UserId = "sa";
        Password = "default";
        Database = "default";
        Port = 1433;
        MinPoolSize = 10;
        MaxPoolSize = 100;
    }

    public string GetConnectionString() {
        if (connection is null) {
            var s = new StringBuilder();

            s.Append($"Data Source={DataSource};");
            s.Append($"Initial Catalog={Database};");
            s.Append($"User ID={UserId};");
            s.Append($"Password={Password};");
            s.Append($"Pooling=true;");
            s.Append($"Min Pool Size={MinPoolSize};");
            s.Append($"Max Pool Size={MaxPoolSize};");

            connection = s.ToString();
        }

        return connection;
    }
}