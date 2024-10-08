using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class ZarValueSupplier : IZarValueSupplier
{
  public List<decimal> CalculateZarValues(List<decimal> percentages, decimal totalCapital)
  {
    List<decimal> zarValues = [];
    decimal value;

    while (percentages.Count != 0)
    {
      value = CalculateZarValue(percentages[0],totalCapital);
      zarValues.Add(value);
      totalCapital -= value;
      percentages.Remove(percentages[0]);
      
      if (percentages.Count == 1)
      {
        value = totalCapital;
        zarValues.Add(value);
        percentages.Remove(percentages[0]);
      }
    }
    return zarValues;
  }

  private static decimal CalculateZarValue(decimal percentage, decimal totalCapital)
    => percentage * totalCapital;
}