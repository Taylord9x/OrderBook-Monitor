namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface IZarValueSupplier
{
  /// <summary>
  /// Calculates the fund's Zar Value allocations based on calculated percentages and total capital.
  /// </summary>
  /// <param name="percentages">List of calculated percentages for each symbol</param>
  /// <param name="totalCapital">The total capital available for the fund</param>
  /// <returns>A list of Zar Value amounts</returns>
  public List<decimal> CalculateZarValues(List<decimal> percentages, decimal totalCapital);
}