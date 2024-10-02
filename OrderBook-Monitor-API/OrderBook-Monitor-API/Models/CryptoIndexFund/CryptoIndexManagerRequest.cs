namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class CryptoIndexManagerRequest
{
  public decimal TotalCapital { get; set; }
  public decimal AssetCap { get; set; }
  public required IEnumerable<Asset> Assets { get; set; }
}