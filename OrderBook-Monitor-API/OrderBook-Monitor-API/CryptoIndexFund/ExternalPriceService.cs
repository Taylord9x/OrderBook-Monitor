using System.Text.Json;
using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;

namespace OrderBook_Monitor_API.CryptoIndexFund;

public class ExternalPriceService : IExternalPriceService
{
  private const string ExternalPriceServiceUri = "https://api.binance.com/api/v3/";
  private const string InternalPriceServiceUri = "http://orderbookmonitorapi:8080/";
  private const string ExternalSkeletonUrl = "depth?symbol={0}USDT&limit=1";
  private const string InternalSkeletonUrl = "api/price/get-price?quantity={0}";

  public async Task<decimal> GetPrice(string symbol)
  {
    decimal usdtPrice = await GetUsdtPrice(symbol);
    decimal zarPrice = await GetZarPrice(usdtPrice);
    return zarPrice;
  }

  private static async Task<decimal> GetZarPrice(decimal usdtPrice)
  {
    HttpClient zarPriceClient = new()
    {
      BaseAddress = new Uri(InternalPriceServiceUri),
    };
    string url = string.Format(InternalSkeletonUrl, usdtPrice.ToString());
    HttpResponseMessage response = await zarPriceClient.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
      var jsonResponse = await response.Content.ReadAsStringAsync();
      var zarPriceData = JsonSerializer.Deserialize<InternalPriceServiceResponse>(jsonResponse);

      return zarPriceData?.Price ?? throw new Exception("Price data not found in response");
    }
    throw new Exception("Unable to fetch ZAR price data");
  }

  private static async Task<decimal> GetUsdtPrice(string symbol)
  {
    HttpClient priceServiceClient = new()
    {
      BaseAddress = new Uri(ExternalPriceServiceUri),
    };

    string url = string.Format(ExternalSkeletonUrl, symbol);
    HttpResponseMessage response = await priceServiceClient.GetAsync(url);

    if (response.IsSuccessStatusCode)
    {
      var jsonResponse = await response.Content.ReadAsStringAsync();
      var priceData = JsonSerializer.Deserialize<ExternalPriceServiceResponse>(jsonResponse);

      if (priceData?.Bids != null && priceData.Bids.Length > 0)
        return decimal.Parse(priceData.Bids[0][0]);
    }
    throw new Exception("Unable to fetch price data");
  }
}