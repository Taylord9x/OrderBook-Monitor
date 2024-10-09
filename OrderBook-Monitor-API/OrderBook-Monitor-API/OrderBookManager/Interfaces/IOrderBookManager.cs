using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.Models.ExternalOrderBookData;

namespace OrderBook_Monitor_API.OrderBookManager.Interfaces;

public interface IOrderBookManager
{
  void AddAsk(Order order);

  void ProcessOrderBookSnapshot(ExternalOrderBook externalOrderBook);
  List<ExternalOrderBookEntry> GetTop25Asks();
  List<ExternalOrderBookEntry> GetTop25Bids();
}