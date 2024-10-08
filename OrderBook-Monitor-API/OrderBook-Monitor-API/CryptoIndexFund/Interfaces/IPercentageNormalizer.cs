namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface IPercentageNormalizer
{
  /// <summary>
  /// Normalizes the fund's total percentage allocation using the calculated zar Values
  /// </summary>
  /// <param name="calculatedZarValues">The calculated amounts using previous percentages and total capital</param>
  /// <returns>A list of normalized percentage distributions (All add up to One)</returns>
  public List<decimal> NormalizePercentages(List<decimal> calculatedZarValues);
}