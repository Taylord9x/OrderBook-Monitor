using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface ICryptoIndexManager
{
  /// <summary>
  /// Calculates the fund's allocation based on market capitalization and asset cap.
  /// </summary>
  /// <param name="totalCapital">The total capital available for the fund.</param>
  /// <param name="assetCap">The maximum percentage any asset can have in the fund.</param>
  /// <param name="assets">The list of assets with market cap and price data.</param>
  /// <returns>A list of asset allocations with final amounts, prices, and percentage allocations.</returns>
  Task<IEnumerable<AssetAllocation>> CalculateAllocations(decimal totalCapital, decimal assetCap, IEnumerable<Asset> assets);
}
