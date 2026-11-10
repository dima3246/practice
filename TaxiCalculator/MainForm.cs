using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TaxiCalculator;

internal sealed class MainForm : Form
{
    private readonly TaxiDatabase database;
    private readonly Tariff tariff;
    private readonly TextBox distanceInput = new() { Dock = DockStyle.Fill };
    private readonly TextBox waitInput = new() { Text = "0", Dock = DockStyle.Fill };
    private readonly Label result = new() { Text = "Введите данные поездки.", AutoSize = true };
    private static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru-RU");

    public MainForm(TaxiDatabase database)
    {
        this.database = database;
        tariff = database.GetTariffs()[0];
        Text = "Калькулятор такси — версия 1";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(570, 335);
        MinimumSize = new Size(520, 340);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreateCalculator());
        tabs.TabPages.Add(CreateExperiment());
        Controls.Add(tabs);
    }

    private TabPage CreateCalculator()
    {
        var page = new TabPage("Расчёт");
        var form = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(16),
            AutoScroll = true
        };
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        form.Controls.Add(new Label { Text = "Расстояние, км", AutoSize = true }, 0, 0);
        form.Controls.Add(distanceInput, 1, 0);
        form.Controls.Add(new Label { Text = "Ожидание, мин", AutoSize = true }, 0, 1);
        form.Controls.Add(waitInput, 1, 1);

        var rates = new Label
        {
            Text = $"{tariff.Name}: подача {tariff.BasePrice} ₽, "
                + $"{tariff.PricePerKm} ₽/км, {tariff.PricePerMinute} ₽/мин ожидания.",
            AutoSize = true,
            Margin = new Padding(3, 12, 3, 8)
        };
        form.Controls.Add(rates, 0, 2);
        form.SetColumnSpan(rates, 2);

        var calculateButton = new Button { Text = "Рассчитать", AutoSize = true };
        calculateButton.Click += Calculate;
        form.Controls.Add(calculateButton, 1, 3);
        form.Controls.Add(result, 0, 4);
        form.SetColumnSpan(result, 2);
        page.Controls.Add(form);
        AcceptButton = calculateButton;
        return page;
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

    private TabPage CreateExperiment()
    {
        var page = new TabPage("Эксперимент");
        var grid = CreateGrid();
        grid.Columns.Add("Distance", "Расстояние, км");
        grid.Columns.Add("Total", "Стоимость по тарифу «Эконом»");
        foreach (int distance in new[] { 1, 5, 10, 20 })
        {
            grid.Rows.Add(distance, Money(tariff.Calculate(distance, 0)));
        }

        page.Controls.Add(grid);
        page.Controls.Add(new Label
        {
            Text = "Меняем расстояние. Ожидание во всех расчётах — 0 минут.",
            Dock = DockStyle.Top,
            Height = 28,
            TextAlign = ContentAlignment.MiddleLeft
        });
        return page;
    }

    private static DataGridView CreateGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            RowHeadersVisible = false
        };
    }

    private static string Money(decimal amount) => amount.ToString("N2", Russian) + " ₽";
}
