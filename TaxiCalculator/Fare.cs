namespace TaxiCalculator;

internal record Tariff(string Name, int BasePrice, int PricePerKm, int PricePerMinute)
{
    public decimal Calculate(decimal distance, int waitMinutes)
    {
        return BasePrice + distance * PricePerKm + waitMinutes * PricePerMinute;
    }
}
