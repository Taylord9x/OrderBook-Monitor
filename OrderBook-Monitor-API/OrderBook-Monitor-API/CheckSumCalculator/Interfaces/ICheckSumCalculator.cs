using OrderBook_Monitor_API.Models.ExternalOrderBookData;

namespace OrderBook_Monitor_API.CheckSumCalculator.Interfaces;

public interface ICheckSumCalculator
{
  long CalculateChecksum(List<ExternalOrderBookEntry> bids, List<ExternalOrderBookEntry> asks);
}