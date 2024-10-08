using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund.Builders;

public static class AssetAllocationBuilder
{
  public static List<AssetAllocation> Build(
    List<string> symbols, 
    List<decimal> amounts, 
    List<decimal> zarValues, 
    List<decimal> percentages)
  {
    List<AssetAllocation> assetAllocations = [];

    for (int i = 0; i < symbols.Count; i++)
    {
      AssetAllocation assetAllocation = new()
      {
        Symbol = symbols[i],
        Amount = Math.Round(amounts[i], 6),
        ZarValue = Math.Round(zarValues[i], 2),
        Percentage = Math.Round(percentages[i], 4)
      };
      assetAllocations.Add(assetAllocation);
    }
    return assetAllocations;
  }
}