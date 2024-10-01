using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;

namespace OrderBook_Monitor_API.OrderBookManager;

public class OrderBookManager(SortedDictionary<decimal, List<Order>> asks) : IOrderBookManager
{
  private readonly SortedDictionary<decimal, List<Order>> _asks = asks;

  public void AddAsk(Order order)
  {
    if (_asks.TryGetValue(order.Price, out List<Order>? value))
    {
      value.Add(order);
    }
    else
    {
      _asks[order.Price] = [order];
    }
  }

  public void ProcessOrderBookSnapshot(ExternalOrderBook externalOrderBook)
  {
    foreach (var externalAsk in externalOrderBook.Data.Asks)
    {
      if (decimal.TryParse(externalAsk.Price, out decimal price))
      {
        foreach (var externalOrder in externalAsk.Orders)
        {
          if (decimal.TryParse(externalOrder.Quantity, out decimal quantity))
          {
            var order = new Order
            {
              Price = price,
              Quantity = quantity
            };
            AddAsk(order);
          }
        }
      }
    }
  }
}