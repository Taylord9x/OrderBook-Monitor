using System.Text.Json.Serialization;

namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class ExternalPriceServiceResponse
{
  [JsonPropertyName("lastUpdateId")]
  public long LastUpdateId { get; set; }
  
  [JsonPropertyName("bids")]
  public required string[][] Bids {get; set;}
  
  [JsonPropertyName("asks")]
  public required string[][] Asks {get; set;}
}