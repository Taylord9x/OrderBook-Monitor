using OrderBook_Monitor_API.CryptoIndexFund.Builders;
using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class CryptoIndexManager(
  ISymbolsExtractor symbolsExtractor,
  IPercentageSupplier percentageSupplier,
  IZarValueSupplier zarValueSupplier,
  IPercentageNormalizer percentageNormalizer,
  IAmountSupplier amountSupplier) : ICryptoIndexManager
{
  private readonly ISymbolsExtractor _symbolsExtractor = symbolsExtractor;
  private readonly IPercentageSupplier _percentageSupplier = percentageSupplier;
  private readonly IZarValueSupplier _zarValueSupplier = zarValueSupplier;
  private readonly IPercentageNormalizer _percentageNormalizer = percentageNormalizer;
  private readonly IAmountSupplier _amountSupplier = amountSupplier;

  public async Task<IEnumerable<AssetAllocation>> CalculateAllocations(decimal totalCapital, decimal assetCap, IEnumerable<Asset> assets)
  {
    List<Asset> assetList = [.. assets.OrderByDescending(asset => asset.MarketCap)];

    List<string> symbols = _symbolsExtractor.GetSymbols([.. assetList]);
    List<decimal> calculatedPercentages = _percentageSupplier.CalculatePercentages([.. assetList], assetCap); 
    List<decimal> calculatedZarValues = _zarValueSupplier.CalculateZarValues(calculatedPercentages, totalCapital);
    
    calculatedPercentages = _percentageNormalizer.NormalizePercentages([.. calculatedZarValues]);
    List<decimal> calculatedAmounts = await _amountSupplier.CalculateAmounts([.. calculatedZarValues], [.. assetList]);
    List<AssetAllocation> assetAllocations = AssetAllocationBuilder.Build(symbols, calculatedAmounts, calculatedZarValues, calculatedPercentages);
    
    return assetAllocations.AsEnumerable();
  }
}