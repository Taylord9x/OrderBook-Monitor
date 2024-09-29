namespace OrderBook_Monitor_API.Models.ExternalOrderBookData;

public class ExternalOrder
{
  public required string OrderId { get; set; }
  public required string Quantity { get; set; }
}