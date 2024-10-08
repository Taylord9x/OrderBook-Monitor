using System.Text.Json.Serialization;

namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class InternalPriceServiceResponse
{
  [JsonPropertyName("price")]
  public required decimal Price {get; set;}
}