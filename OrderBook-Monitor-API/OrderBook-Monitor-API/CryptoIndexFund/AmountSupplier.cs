using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class AmountSupplier(IExternalPriceService externalPriceService) : IAmountSupplier
{
  private readonly IExternalPriceService _externalPriceService = externalPriceService;
  
  public async Task<List<decimal>> CalculateAmounts(List<decimal> zarValues, List<Asset> assets)
  {
    List<decimal> calculatedAmounts = [];
    while (zarValues.Count != 0)
    {
      decimal assetPrice = assets[0].Price;
      
      if (assetPrice == 0)
        assetPrice = await _externalPriceService.GetPrice(assets[0].Symbol);
      
      calculatedAmounts.Add(CalculateAmount(zarValues[0],assetPrice));
      
      zarValues.Remove(zarValues[0]);
      assets.Remove(assets[0]);
    }
    return calculatedAmounts;
  }

  private static decimal CalculateAmount(decimal zarValue, decimal assetPrice)
    => zarValue / assetPrice;
}