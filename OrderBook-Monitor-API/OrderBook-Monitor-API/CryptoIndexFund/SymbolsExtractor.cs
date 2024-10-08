using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class SymbolsExtractor : ISymbolsExtractor
{
  public List<string> GetSymbols(List<Asset> assets)
  {
    List<string> symbols = [];
    
    foreach (var asset in assets)
      symbols.Add(asset.Symbol);

    return symbols;
  }
}