using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface IPercentageSupplier
{
  /// <summary>
  /// Calculates the fund's percentage allocations only based on assets and the asset cap.
  /// </summary>
  /// <param name="assetCap">The maximum percentage any asset can have in the fund.</param>
  /// <param name="assets">The list of assets with market cap and price data.</param>
  /// <returns>A list of asset with their correct percentage allocations.</returns>
  public List<decimal> CalculatePercentages(List<Asset> assets, decimal assetCap);
}