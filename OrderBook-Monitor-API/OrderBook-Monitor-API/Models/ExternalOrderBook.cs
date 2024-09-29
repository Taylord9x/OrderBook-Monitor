using OrderBook_Monitor_API.Models.ExternalOrderBookData;

namespace OrderBook_Monitor_API.Models;

public class ExternalOrderBook
{
  public required string Type { get; set; }
  public required string CurrencyPairSymbol { get; set; }
  public required ExternalOrderBookMetaData Data { get; set; }
}