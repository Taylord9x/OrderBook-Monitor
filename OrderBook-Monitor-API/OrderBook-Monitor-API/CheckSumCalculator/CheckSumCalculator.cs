using System.Text;
using OrderBook_Monitor_API.CheckSumCalculator.Interfaces;
using OrderBook_Monitor_API.Models.ExternalOrderBookData;

namespace OrderBook_Monitor_API.CheckSumCalculator;
public class CheckSumCalculator : ICheckSumCalculator
{
  public long CalculateChecksum(List<ExternalOrderBookEntry> bids, List<ExternalOrderBookEntry> asks)
  {
    var sb = new StringBuilder();

    foreach (var bid in bids)
    {
      sb.Append(bid.Price);
      sb.Append(bid.Orders[0].Quantity);
    }

    foreach (var ask in asks)
    {
      sb.Append(ask.Price);
      sb.Append(ask.Orders[0].Quantity);
    }
    return CalculateCRC32(Encoding.UTF8.GetBytes(sb.ToString()));
  }

  private static uint CalculateCRC32(byte[] data)
  {
    uint crc = 0xFFFFFFFF;
    for (int i = 0; i < data.Length; i++)
    {
      crc ^= data[i];
      for (int j = 0; j < 8; j++)
      crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;
    }
    return ~crc;
  }
}