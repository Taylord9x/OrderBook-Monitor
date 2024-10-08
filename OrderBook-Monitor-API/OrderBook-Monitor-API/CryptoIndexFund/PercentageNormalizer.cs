using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class PercentageNormalizer : IPercentageNormalizer
{
  public List<decimal> NormalizePercentages(List<decimal> calculatedZarValues)
  {
    List<decimal> normalizedPercentages = [];

    foreach (var value in calculatedZarValues)
      normalizedPercentages.Add(NormalizePercentage(value));
    
    return normalizedPercentages;
  }

  private static decimal NormalizePercentage(decimal value)
    => value / 10;
}