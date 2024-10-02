using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class CryptoIndexManager : ICryptoIndexManager
{
  public Task<IEnumerable<AssetAllocation>> CalculateAllocations(decimal totalCapital, decimal assetCap, IEnumerable<Asset> assets)
  {
    var assetList = assets.ToList();
    List<AssetAllocation> assetAllocations = [];
    AssetAllocation assetAllocation;
    decimal totalMarketCap, percentage, zarValue;
    int counter = 0;

    while (assetList.Count != 0)
    {
      totalMarketCap = assetList.Sum(asset => asset.MarketCap);
      foreach (var asset in assetList.ToList())
      {
        percentage = CalculatePercentage(totalMarketCap, asset.MarketCap);
        
        if ((percentage > assetCap) && (counter == 0))
          percentage = assetCap;

        zarValue = CalculateZarValue(percentage, totalCapital);
        
        if (counter != 0)
          percentage = zarValue / 1000;

        percentage = Math.Round(percentage, 4);
        zarValue = Math.Round(zarValue, 2);
        
        assetAllocation = new AssetAllocation
        {
          Symbol = asset.Symbol,
          Price = asset.Price,
          Percentage = percentage * 100,
          Amount = zarValue
        };

        assetAllocations.Add(assetAllocation);
        assetList.Remove(asset);
        totalCapital -= zarValue;
        counter++;
        break;
      }
    }
    return Task.FromResult(assetAllocations.AsEnumerable());
  }

  private static decimal CalculatePercentage(decimal totalMarketCap, decimal marketCap)
    => marketCap / totalMarketCap;

  private static decimal CalculateZarValue(decimal percentage, decimal totalCapital)
    => percentage * totalCapital;
}