namespace OrderBook_Monitor_API.Models;

public class Order
{
  public required string OrderId {get; set;}
  public decimal Price {get; set;}
  public decimal Quantity {get; set;}
}