using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TaxiCalculator;

public partial class MainForm : Form
{
    private TaxiDatabase database = null!;
    private Tariff[] tariffs = [];
    private static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru-RU");

    public MainForm()
    {
        InitializeComponent();
    }

    internal MainForm(TaxiDatabase database) : this()
    {
        this.database = database;
        tariffs = database.GetTariffs();

        tariffInput.Items.AddRange(tariffs.Select(tariff => tariff.Name).ToArray());
        tariffInput.SelectedIndex = 0;
        UpdateRates();

        foreach (Tariff tariff in tariffs)
            experimentGrid.Columns.Add(tariff.Name, tariff.Name);

        UpdateExperiment();
        tariffInput.SelectedIndexChanged += (_, _) => UpdateRates();
        calculateButton.Click += Calculate;
        tabs.Selected += ShowHistory;
        experimentWait.ValueChanged += (_, _) => UpdateExperiment();
    }

    private void UpdateRates()
    {
        Tariff tariff = tariffs[tariffInput.SelectedIndex];
        rates.Text = $"Подача {tariff.BasePrice} ₽, {tariff.PricePerKm} ₽/км, "
            + $"{tariff.PricePerMinute} ₽/мин ожидания.";
        result.Text = "Введите данные поездки.";
    }

    private void Calculate(object? sender, EventArgs e)
    {
        string distanceText = distanceInput.Text.Trim().Replace(',', '.');
        if (!decimal.TryParse(distanceText, NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out decimal distance)
            || distance <= 0 || distance > 1000 || distance != decimal.Round(distance, 2))
        {
            result.Text = "Введите расстояние от 0,01 до 1000 км (до двух знаков после запятой).";
            distanceInput.Focus();
            return;
        }

        if (!int.TryParse(waitInput.Text, out int waitMinutes) || waitMinutes < 0 || waitMinutes > 1440)
        {
            result.Text = "Введите целое число минут ожидания от 0 до 1440.";
            waitInput.Focus();
            return;
        }

        Tariff tariff = tariffs[tariffInput.SelectedIndex];
        decimal total = tariff.Calculate(distance, waitMinutes);
        try
        {
            database.SaveCalculation(tariff, distance, waitMinutes, total);
            result.Text = $"Стоимость: {Money(total)}. Расчёт сохранён.";
        }
        catch (SqliteException)
        {
            result.Text = $"Стоимость: {Money(total)}. Не удалось сохранить расчёт.";
        }
    }

    private void ShowHistory(object? sender, TabControlEventArgs e)
    {
        if (e.TabPage != historyPage)
            return;

        historyGrid.Rows.Clear();
        try
        {
            List<Calculation> calculations = database.GetRecentCalculations();
            foreach (Calculation entry in calculations)
            {
                historyGrid.Rows.Add(entry.Time.ToString("dd.MM.yyyy HH:mm", Russian), entry.TariffName,
                    entry.Distance.ToString("0.##", Russian), entry.WaitMinutes, Money(entry.Total));
            }

            historyStatus.Text = calculations.Count == 0
                ? "Расчётов пока нет."
                : "Последние расчёты (до 100 записей)";
        }
        catch (SqliteException)
        {
            historyStatus.Text = "Не удалось загрузить историю.";
        }
    }

    private void UpdateExperiment()
    {
        experimentGrid.Rows.Clear();
        foreach (int distance in new[] { 1, 5, 10, 20 })
        {
            var row = new object[tariffs.Length + 1];
            row[0] = distance;
            for (int index = 0; index < tariffs.Length; index++)
                row[index + 1] = Money(tariffs[index].Calculate(distance, (int)experimentWait.Value));
            experimentGrid.Rows.Add(row);
        }
    }

    private static string Money(decimal amount) => amount.ToString("N2", Russian) + " ₽";
}
