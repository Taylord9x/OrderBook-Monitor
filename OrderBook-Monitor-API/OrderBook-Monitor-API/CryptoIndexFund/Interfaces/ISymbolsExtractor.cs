using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface ISymbolsExtractor
{
  /// <summary>
  /// Calculates the fund's Zar Value allocations based on calculated percentages and total capital.
  /// </summary>
  /// <param name="assets">The list of assets with their respective symbols</param>
  /// <returns>A list of index symbols</returns>
  public List<string> GetSymbols(List<Asset> assets);
}