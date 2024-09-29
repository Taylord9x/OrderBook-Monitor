using OrderBook_Monitor_API.Models;

namespace OrderBook_Monitor_API.OrderBookManager.Interfaces;

public interface IOrderBookManager
{
  void AddAsk(Order order);

  void ProcessOrderBookSnapshot(ExternalOrderBook externalOrderBook);
}