using Microsoft.Data.Sqlite;

namespace TaxiCalculator;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TaxiCalculator");
            Directory.CreateDirectory(folder);
            var database = new TaxiDatabase(Path.Combine(folder, "taxi.db"));
            database.Initialize();

            Application.Run(new MainForm(database));
        }
        catch (Exception error) when (error is SqliteException or IOException or UnauthorizedAccessException)
        {
            MessageBox.Show($"Не удалось открыть базу данных.\n{error.Message}",
                "Калькулятор такси", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
