namespace OrderBook_Monitor_API.CryptoIndexFund.Interfaces;

public interface IExternalPriceService
{
  /// <summary>
  /// Fetches the price of a given crypto index in ZAR
  /// </summary>
  /// <param name="symbol">The crypto index whose relative price in ZAR we are looking for</param>
  /// <returns>value in decimal</returns>
  Task<decimal> GetPrice(string symbol);
}