using Microsoft.Data.Sqlite;

namespace TaxiCalculator;

internal sealed class TaxiDatabase(string path)
{
    private readonly string connectionString = new SqliteConnectionStringBuilder
    {
        DataSource = path,
        ForeignKeys = true
    }.ToString();

    public void Initialize()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS tariffs (
                name TEXT PRIMARY KEY,
                base_price INTEGER NOT NULL CHECK (base_price >= 0),
                price_per_km INTEGER NOT NULL CHECK (price_per_km >= 0),
                price_per_minute INTEGER NOT NULL CHECK (price_per_minute >= 0)
            );

            CREATE TABLE IF NOT EXISTS calculations (
                id INTEGER PRIMARY KEY,
                created_at TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                tariff TEXT NOT NULL REFERENCES tariffs(name),
                distance_km REAL NOT NULL CHECK (distance_km > 0),
                wait_minutes INTEGER NOT NULL CHECK (wait_minutes >= 0),
                total_kopecks INTEGER NOT NULL CHECK (total_kopecks >= 0)
            );

            INSERT OR IGNORE INTO tariffs (name, base_price, price_per_km, price_per_minute)
            VALUES ('Эконом', 100, 20, 5),
                   ('Комфорт', 160, 30, 7),
                   ('Бизнес', 250, 45, 10);
            """;
        command.ExecuteNonQuery();
        transaction.Commit();
    }

    public Tariff[] GetTariffs()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT name, base_price, price_per_km, price_per_minute
            FROM tariffs
            ORDER BY base_price, name
            """;

        using var reader = command.ExecuteReader();
        var tariffs = new List<Tariff>();
        while (reader.Read())
        {
            tariffs.Add(new Tariff(reader.GetString(0), reader.GetInt32(1),
                reader.GetInt32(2), reader.GetInt32(3)));
        }

        return tariffs.ToArray();
    }

    public void SaveCalculation(Tariff tariff, decimal distance, int waitMinutes, decimal total)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO calculations (tariff, distance_km, wait_minutes, total_kopecks)
            VALUES ($tariff, $distance, $wait, $total)
            """;
        command.Parameters.AddWithValue("$tariff", tariff.Name);
        command.Parameters.AddWithValue("$distance", (double)distance);
        command.Parameters.AddWithValue("$wait", waitMinutes);
        command.Parameters.AddWithValue("$total", (long)(total * 100));
        command.ExecuteNonQuery();
    }

    public List<Calculation> GetRecentCalculations()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT datetime(created_at, 'localtime'), tariff, distance_km, wait_minutes, total_kopecks
            FROM calculations
            ORDER BY id DESC
            LIMIT 100
            """;

        var calculations = new List<Calculation>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            calculations.Add(new Calculation(reader.GetDateTime(0), reader.GetString(1),
                (decimal)reader.GetDouble(2), reader.GetInt32(3), reader.GetInt64(4) / 100m));
        }

        return calculations;
    }
}

internal record Calculation(DateTime Time, string TariffName, decimal Distance, int WaitMinutes,
    decimal Total);
