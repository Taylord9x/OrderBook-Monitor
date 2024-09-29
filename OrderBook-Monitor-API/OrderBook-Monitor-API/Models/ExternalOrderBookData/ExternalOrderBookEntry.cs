namespace OrderBook_Monitor_API.Models.ExternalOrderBookData;

public class ExternalOrderBookEntry
{
  public required string Price { get; set; }
  public required List<ExternalOrder> Orders { get; set; }
}