using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface IAmountSupplier
{
  /// <summary>
  /// Calculates the fund's index amount allocations based on the zar Values and the asset list
  /// </summary>
  /// <param name="zarValues">List of Zar Value amounts</param>
  /// <param name="assets">The list of assets with market cap and price data</param>
  /// <returns>A list of Amounts for each symbol</returns>
  public Task<List<decimal>> CalculateAmounts(List<decimal> zarValues, List<Asset> assets);
}