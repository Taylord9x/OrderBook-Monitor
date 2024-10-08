using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class PercentageSupplier : IPercentageSupplier
{
  public List<decimal> CalculatePercentages(List<Asset> assets, decimal assetCap)
  {
    decimal totalMarketCap = 0;
    decimal percentage = 0;
    List<decimal> newPercentagesList = [];
    bool isFirsIteration = true;

    while (assets.Count != 0)
    {
      totalMarketCap = assets.Sum(asset => asset.MarketCap);
      percentage = CalculateAssetPercentage(assets[0],totalMarketCap);
      
      if ((percentage > assetCap) && isFirsIteration)
      {
        percentage = assetCap;
        isFirsIteration = false;
      }

      newPercentagesList.Add(percentage);
      assets.Remove(assets[0]);

      if (assets.Count == 1)
      {
        percentage = 1 - percentage;
        newPercentagesList.Add(percentage);
        assets.Remove(assets[0]);
      }
    }
    return newPercentagesList;
  }

  private static decimal CalculateAssetPercentage(Asset asset, decimal totalMarketCap)
    => asset.MarketCap / totalMarketCap;
}